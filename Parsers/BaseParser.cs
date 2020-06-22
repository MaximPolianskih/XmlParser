using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XmlParser.Parsers
{
    public abstract class BaseParser
    {
        protected virtual async Task<T> ParseXmlToObjectAsync<T>(XmlReader xmlReader) where T : class, new()
        {
            var obj = new T();
            var tagName = xmlReader.Name;

            while (await xmlReader.ReadAsync())
            {
                if (xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    if (xmlReader.Name != tagName)
                    {
                        throw new Exception("Xml not valid.");
                    }

                    break;
                }

                await xmlReader.MoveToContentAsync();
                await ParseXmlDataToObjectAsync(xmlReader, obj);
            }

            return obj;
        }

        protected virtual async Task ParseXmlDataToObjectAsync<T>(XmlReader reader, T obj) where T : class, new()
        {
            var property = typeof(T).GetProperty(reader.Name);

            //Пропускаем неопределенные поля
            if (property == null)
            {
                await reader.SkipAsync();
                return;
            }

            //Проверяем на вложенность
            if (property.PropertyType.IsClass && !string.IsNullOrEmpty(GetElementName(property.PropertyType)))
            {
                var nestedObj = await ParseNestedObject(reader, property.PropertyType);
                property.SetValue(obj, nestedObj);

                return;
            }

            //Для каждого поля получаем конвертер соответствующего типа
            var converter = TypeDescriptor.GetConverter(property.PropertyType);
            //Получаем соответствующее значение из xml и приводим его к типу поля
            var value = converter.ConvertFromString((await reader.ReadElementContentAsStringAsync()).Trim());
            //Записываем значение поля в объект
            property.SetValue(obj, value);
        }
        private async Task<object> ParseNestedObject(XmlReader reader, Type propertyType)
        {
            //Для вложенного объекта вызываем парсинг
            var createManagerMethod = this.GetType().GetMethod(nameof(ParseXmlToObjectAsync), BindingFlags.NonPublic | BindingFlags.Instance);
            var method = createManagerMethod.MakeGenericMethod(propertyType);
            var task = (Task)method.Invoke(this, new object[] { reader });
            await task;

            var resultProperty = typeof(Task<>).MakeGenericType(propertyType).GetProperty("Result");
            var subObj = resultProperty.GetValue(task);

            return subObj;
        }

        protected string GetElementName<T>() where T : class, new()
        {
            return typeof(T).GetCustomAttribute<XmlRootAttribute>()?.ElementName;
        }
        protected string GetElementName(System.Type type)
        {
            return ((XmlRootAttribute)type.GetCustomAttribute(typeof(XmlRootAttribute)))?.ElementName;
        }
    }
}
