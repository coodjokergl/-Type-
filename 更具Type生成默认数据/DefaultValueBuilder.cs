using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 更具Type生成默认数据
{
    public class DefaultValueBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Build(Type type)
        {
            var objValue = Activator.CreateInstance(type);

            BuildImpl(objValue);

            return objValue;
        }

        /// <summary>
        /// 属性设置实例
        /// </summary>
        /// <param name="value"></param>
        private void BuildImpl(object value)
        {
            var propertys = GetPropertys(value);

            while (true)
            {
                var nextPropertys = new List<MyPrpertyInfo>();

                //获取下级的
                foreach (var info in propertys)
                {
                    nextPropertys.AddRange(GetPropertys(info.Value));
                }

                //设置数据
                foreach (var info in propertys)
                {
                    info.PropertyInfo.SetValue(info.Owner,info.DefaultValue);
                }

                //构建下次循环条件
                propertys = nextPropertys;
                if (!CanNext(propertys))
                {
                    break;
                }
            }
        }


        private List<MyPrpertyInfo> GetPropertys(object value)
        {
            return value.GetType().GetProperties().Where(q => q.CanWrite).Select(q =>
                new MyPrpertyInfo()
                {
                    PropertyInfo = q,
                    Owner = value
                }).ToList();
        }

        private bool CanNext(List<MyPrpertyInfo> propertys)
        {
            return propertys.Exists(q => q.Value.GetType().IsClass);
        }
    }

    class MyPrpertyInfo
    {
        private PropertyInfo _propertyInfo;


        public object Owner { get; set; }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set
            {
                _propertyInfo = value;
                SetValue();
            }
        }

        /// <summary>
        /// 真正的值
        ///  OrderDto
        ///  List<OrderDto> (1)  
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object DefaultValue { get; set; }

        private void SetValue()
        {
            if (_propertyInfo.PropertyType.IsGenericType)
            {
                //
            }
            else
            {
                DefaultValue = CreateDefaultValue(_propertyInfo.PropertyType);
                Value = DefaultValue;
            }
        }


        private object CreateDefaultValue(Type type)
        {
            if (_valueProvider.TryGetValue(type, out Func<object> gettter))
            {
                return gettter();
            }
            return Activator.CreateInstance(type);
        }


        private Dictionary<Type,Func<object>> _valueProvider = new Dictionary<Type, Func<object>>()
        {
            {typeof(string),()=>"老王"},
            {typeof(int),()=> 123},
            {typeof(DateTime),()=>DateTime.Now},
            {typeof(decimal),()=>123.123}
        };
    }
}
