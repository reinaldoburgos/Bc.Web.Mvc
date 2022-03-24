using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Collections;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
//using Bc.Core.Data;
//using Bc.Data.Entity;
//using Bc.Data;


namespace Bc.Constants
{
    public sealed class Xml
    {

        public static string UnicodeId
        {
            //get { return "UTF-16"; }
           // get { return "UTF-8"; }
            get { return ""; }
        }

        public static string Version
        {
            get { return "1.0"; }
        }

    }

    public sealed class Format
    {
        static internal string PrefixKey
        {
            get { return "K"; }
        }

        public static string Separator
        {
            get { return "~"; }
        }

        public static char NewLine => '\n';

        public static string BackSlash
        {
            get { return "\\"; }
        }

        public static string Lines()
        {
            return Lines(2);
        }

        public static string Lines(int number)
        {
            string returnValue = string.Empty;
            for (int ind = 1; ind <= number; ind++)
            {
                returnValue += NewLine; // '\n'; // Microsoft.VisualBasic.ControlChars.NewLine; TODO: modificado para que funcione
            }

            return returnValue;
        }

        public static string Quotes
        {
            get { return "\""; }
        }

        public static string BeginTag
        {
            get { return "&lt;"; }
        }

        public static string EndTag
        {
            get { return "&gt;"; }
        }

        public static string Apostrophe
        {
            get { return "&apos;"; }
        }

        public static string QuotationMark
        {
            get { return "&quot;"; }
        }

        public static string Ampersand
        {
            get { return "&amp;"; }
        }

        public static string ChartViewerTitle
        {
            get { return "{1}{0}{0}'{2}', agrupado por '{3}',{0}aplicando la función '{4}' y graficando como '{5}'"; }
        }

        private static System.Globalization.CultureInfo _SqlCultureInfo;
        public static System.Globalization.CultureInfo SqlCultureInfo
        {
            get
            {
                if (_SqlCultureInfo == null)
                {
                    _SqlCultureInfo = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.LCID);
                    _SqlCultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.CurrencyGroupSeparator = ",";
                    _SqlCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.NumberGroupSeparator = ",";
                    _SqlCultureInfo.NumberFormat.PercentDecimalSeparator = ".";
                    _SqlCultureInfo.NumberFormat.PercentGroupSeparator = ",";

                    _SqlCultureInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";

                }
                return _SqlCultureInfo;
            }
        }

    }
}

namespace Bc.Xml
{    
    #region " XmlPropertiesInfo "

    /// <summary>
    /// Provee información para especializar la generación de XML a partir de los elementos contenidos en una colección. (Ver el método <b>ToXml</b> de <see cref="GenericCollection(Of T)"></see>)
    /// </summary>
    /// <remarks></remarks>
    public class XmlPropertiesInfo : System.ComponentModel.BindingList<string>
    {

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see>.
        /// </summary>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="String"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(Type memberType, params string[] propertyNames)
            : this(string.Empty, string.Empty, memberType, propertyNames)
        {
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see> indicando el nombre de elemento.
        /// </summary>
        /// <param name="elementName">Un <see cref="String"></see> que representa el nombre de elemento que se usará para identificar el tipo en la generación del XML.</param>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="String"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(string elementName, Type memberType, params string[] propertyNames)
            : this(string.Empty, elementName, memberType, propertyNames)
        {
        }

        /// <summary>
        /// Inicializa una instancia de <see cref="XmlPropertiesInfo"></see> indicando el nombre de elemento.
        /// </summary>
        /// <param name="memberName">Un <see cref="String"></see> que identifica al miembro a inlcuir en la generación del XML.</param>
        /// <param name="elementName">Un <see cref="String"></see> que representa el nombre de elemento que se usará para identificar el tipo en la generación del XML.</param>
        /// <param name="memberType">Un <see cref="Type"></see> que representa el tipo de elemento que se desea incluir en la generación del XML.</param>
        /// <param name="propertyNames">Una serie de <see cref="String"></see> que representan los nombres de las propiedades del tipo de elementos que se desea incluir en la generación del XML.</param>
        /// <remarks></remarks>
        public XmlPropertiesInfo(string memberName, string elementName, Type memberType, params string[] propertyNames)
            : base()
        {
            if ((memberName != null))
            {
                this._MemberName = memberName.Trim();
            }
            if ((elementName != null))
            {
                this._ElementName = elementName.Trim();
            }
            this._MemberType = memberType;
            if (propertyNames.Length > 0)
            {
                foreach (string propertyName in propertyNames)
                {
                    this.Add(propertyName);
                }
            }
            else if (memberType != null)
            {
                foreach (PropertyInfo info in memberType.GetProperties())
                {
                    this.Add(info.Name);
                }
            }
        }

        private Type _MemberType;
        /// <summary>
        /// Obtiene el valor que representa el tipo de elemento que debe ser incluído en la generación del XML.
        /// </summary>
        /// <value>Un <see cref="Type"></see>.</value>
        /// <returns>El tipo de elemento a ser incluído en la generación del XML.</returns>
        /// <remarks></remarks>
        public Type MemberType
        {
            get { return _MemberType; }
        }

        private string _MemberName = string.Empty;
        /// <summary>
        /// Obtiene el nombre del miembro que identifica de manera única la definición del elemento a incluir en la generación XML.
        /// </summary>
        public string MemberName
        {
            get { return _MemberName; }
        }

        /// <summary>
        /// Obtiene el nombre de elemento con el cual se identifica el tipo en la generación XML.
        /// </summary>
        private string _ElementName = string.Empty;
        public string ElementName
        {
            get { return _ElementName; }
        }

        /// <summary>
        /// Asigna el tipo de elemento.
        /// </summary>
        /// <param name="memberType">Un <see cref="Type"></see>.</param>
        /// <remarks></remarks>
        internal void SetMemberType(Type memberType)
        {
            _MemberType = memberType;
        }

    }

    #endregion

    #region " XmlParameterConstructorInfo "

    /// <summary>
    /// Provee información que es utilizada para la creación de instancias de elementos de colección, cuando éstos son creados a partir de un XML. (Ver el método <b>LoadXml</b> de <see cref="GenericCollection(Of T)"></see>)
    /// </summary>
    /// <remarks></remarks>
    public class XmlParameterConstructorInfo
    {

        /// <summary>
        /// Crea e inicializa una nueva instancia de <see cref="XmlParameterConstructorInfo"></see>.
        /// </summary>
        /// <param name="type">Un <see cref="Type"></see> que representa el tipo de parámetro que debe ser enviado al constructor del elemento.</param>
        /// <param name="name">Un <see cref="String"></see> que representa el nombre del atributo dentro del XML que debe ser enviado como parámetro al constructor del elemento.</param>
        /// <remarks></remarks>
        public XmlParameterConstructorInfo(Type type, string name)
        {
            _Type = type;
            _Name = name;
        }

        private Type _Type;
        /// <summary>
        /// Obtiene el valor que representa el tipo de parámetro que debe ser enviado al constructor del elemento.
        /// </summary>
        /// <value>Un <see cref="Type"></see>.</value>
        /// <returns>Referencia al tipo de parámetro.</returns>
        /// <remarks></remarks>
        public Type Type
        {
            get { return _Type; }
        }

        private string _Name;
        /// <summary>
        /// Obtiene el valo que representa el nombre del atributo que debe ser enviado como parámetro al constructor del elemento.
        /// </summary>
        /// <value>Un <see cref="String"></see>.</value>
        /// <returns>El nombre del atributo.</returns>
        /// <remarks></remarks>
        public string Name
        {
            get { return _Name; }
        }
    }

    #endregion

    #region " XmlConverter "

    public sealed class XmlConverter
    {

        private XmlConverter()
        {
        }

        #region " Private Shared Methods and Fields "

        private static void CheckPropertiesInfo(Type mainMemberType, Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            if (propertiesInfo == null)
                return;
            if (propertiesInfo.Length == 0)
                return;
            if (propertiesInfo.Length > 1)
            {
                foreach (Xml.XmlPropertiesInfo propertyInfo in propertiesInfo)
                {
                    if (propertyInfo.MemberType == null)
                    {
                        throw new ArgumentNullException("MemberType");
                        return;
                    }
                }
            }
            else
            {
                if (propertiesInfo[0].MemberType == null)
                    propertiesInfo[0].SetMemberType(mainMemberType);
            }
        }

        private static bool IncludeProperty(PropertyInfo propertyInfo)
        {
            //TODO: comentado para que funcione

            return true;

            //if (!propertyInfo.IsDefined(typeof(NonSerializableToXmlAttribute), true))
            //{
            //    PropertyDescriptorCollection restrictProperties = TypeDescriptor.GetProperties(typeof(EntityBase));
            //    string[] allowProperties = {
            //        "ID",
            //        "DESCRIPTION",
            //        "KEY"
            //    };
            //    if (Array.IndexOf<string>(allowProperties, propertyInfo.Name.ToUpper()) != -1)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return restrictProperties.Find(propertyInfo.Name, true) == null;
            //    }
            //}
            //return false;
        }

        private static bool IncludeProperty(Type parentType, string memberName, Type memberType, PropertyInfo propertyInfo, Xml.XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            if (xmlPropertiesInfo == null || xmlPropertiesInfo.Length == 0)
            {
                return IncludeProperty(propertyInfo);
            }
            else if (xmlPropertiesInfo.Length > 0)
            {
                Xml.XmlPropertiesInfo info = FindPropertiesInfo(parentType, memberName, memberType, xmlPropertiesInfo);
                if (info == null)
                {
                    foreach (Xml.XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                    {
                        if (object.ReferenceEquals(xmlPropertyInfo.MemberType, parentType) && xmlPropertyInfo.IndexOf(memberName) != -1)
                        {
                            return IncludeProperty(propertyInfo);
                        }
                    }
                }
                else if (IncludeProperty(propertyInfo))
                {
                    return info.IndexOf(propertyInfo.Name) != -1;
                }
                return false;
            }

            return false;
        }

        private static Xml.XmlPropertiesInfo FindPropertiesInfo(Type parentType, string memberName, Type memberType, Xml.XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            if (xmlPropertiesInfo != null)
            {
                foreach (Xml.XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                {
                    if (object.ReferenceEquals(xmlPropertyInfo.MemberType, memberType))
                    {
                        if (string.IsNullOrEmpty(memberName) || (string.IsNullOrEmpty(xmlPropertyInfo.MemberName) && string.IsNullOrEmpty(xmlPropertyInfo.ElementName)))
                        {
                            return xmlPropertyInfo;
                        }
                        if (memberName == xmlPropertyInfo.MemberName)
                        {
                            return xmlPropertyInfo;
                        }
                        if (memberName == xmlPropertyInfo.ElementName)
                        {
                            return xmlPropertyInfo;
                        }
                    }
                }
                //JCardenas: 21-08-2009
                //Permite que se pueda realizar una conversión a Xml de una clase cabecera haciendo referencia a un tipo de clase base o interfaz compatible, no
                //necesariamente el mismo tipo de la clase.
                if (parentType == null)
                {
                    foreach (Xml.XmlPropertiesInfo xmlPropertyInfo in xmlPropertiesInfo)
                    {
                        //Verifica si el tipo memberType es equivalente al tipo declarado en xmlPropertyInfo
                        if (memberType.IsSubclassOf(xmlPropertyInfo.MemberType) || xmlPropertyInfo.MemberType.IsAssignableFrom(memberType))
                        {
                            if (string.IsNullOrEmpty(memberName) || (string.IsNullOrEmpty(xmlPropertyInfo.MemberName) && string.IsNullOrEmpty(xmlPropertyInfo.ElementName)))
                            {
                                return xmlPropertyInfo;
                            }
                            if (memberName == xmlPropertyInfo.MemberName)
                            {
                                return xmlPropertyInfo;
                            }
                            if (memberName == xmlPropertyInfo.ElementName)
                            {
                                return xmlPropertyInfo;
                            }
                        }
                    }
                }
            }
            return null;
        }

        private static string GetElementName(Type parentType, string memberName, Type memberType, Xml.XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            string returnValue = string.Empty;
            Xml.XmlPropertiesInfo propInfo = FindPropertiesInfo(parentType, memberName, memberType, xmlPropertiesInfo);
            if ((propInfo != null))
                returnValue = propInfo.ElementName;
            if (string.IsNullOrEmpty(returnValue))
            {
                string[] names = memberType.Name.Split('`');
                returnValue = Convert.ToString((names.Length > 0 ? names[0] : memberType.Name));
            }
            return returnValue;
        }

        static internal XmlElement CreateXmlElement(XmlDocument xmlDocument, string memberName, object value, Type parentType, Xml.XmlPropertiesInfo[] xmlPropertiesInfo)
        {
            System.Xml.XmlElement xmlNode = default(System.Xml.XmlElement);
            object currentValue = null;
            //Type type = value.GetType().BaseType;

            Type type = value.GetType();

            if (type.IsSealed)
                type = type.BaseType;

            xmlNode = xmlDocument.CreateElement(GetElementName(parentType, memberName, type, xmlPropertiesInfo));
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            //For i As Integer = properties.Length - 1 to 0 step -1
            //    Dim propertyInfo As PropertyInfo = properties(i)
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.GetIndexParameters().Length == 0)
                {
                    if (IncludeProperty(parentType, memberName, type, propertyInfo, xmlPropertiesInfo) && propertyInfo.CanRead)
                    {
                        try
                        {
                            currentValue = propertyInfo.GetValue(value, null);
                            if ((currentValue != null))
                            {
                                Type propertyType = currentValue.GetType();
                                if (object.ReferenceEquals(propertyType, typeof(bool)))
                                {
                                    if (Convert.ToBoolean(currentValue))
                                    {
                                        xmlNode.SetAttribute(propertyInfo.Name, "1");
                                    }
                                    else
                                    {
                                        xmlNode.SetAttribute(propertyInfo.Name, "0");
                                    }
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(string)) | object.ReferenceEquals(propertyType, typeof(object)))
                                {
                                    //JFCardenas 12-Ago-2006
                                    //Comenté llamada a Format xq xmlNode.SetAttribute realiza el formateo.
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                    // Format(CStr(currentValue)))
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(int)) || object.ReferenceEquals(propertyType, typeof(long)) || object.ReferenceEquals(propertyType, typeof(short)) || object.ReferenceEquals(propertyType, typeof(uint)) || object.ReferenceEquals(propertyType, typeof(ushort)) || object.ReferenceEquals(propertyType, typeof(ulong)) || object.ReferenceEquals(propertyType, typeof(byte)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(decimal)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToDecimal(currentValue).ToString(Bc.Constants.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(double)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToDouble(currentValue).ToString(Bc.Constants.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(float)))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToSingle(currentValue).ToString(Bc.Constants.Format.SqlCultureInfo.NumberFormat));
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(System.DateTime)))
                                {
                                    //If IsDate(currentValue) AndAlso CDate(currentValue) > Date.MinValue Then xmlNode.SetAttribute(propertyInfo.Name, String.Format("{0:yyyy-MM-dd}", currentValue) & " " & CType(currentValue, Date).ToLongTimeString())
                                    //if (Information.IsDate(currentValue) && Convert.ToDateTime(currentValue) > System.DateTime.MinValue)
                                    if (currentValue is DateTime && Convert.ToDateTime(currentValue) > System.DateTime.MinValue)
                                        xmlNode.SetAttribute(propertyInfo.Name, string.Format("{0:yyyyMMdd} {1:HH:mm:ss}", currentValue, currentValue));
                                }
                                else if (propertyType.IsEnum)
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Convert.ToString(currentValue));
                                }
                                else if (object.ReferenceEquals(propertyType, typeof(byte[])))
                                {
                                    //xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize(new byte[] { Convert.ToByte(currentValue) }));
                                    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((byte[])currentValue));
                                }
                                else if (typeof(System.Drawing.Image).IsInstanceOfType(currentValue))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((System.Drawing.Image)currentValue));
                                }
                                else if (typeof(System.Drawing.Icon).IsInstanceOfType(currentValue))
                                {
                                    xmlNode.SetAttribute(propertyInfo.Name, Imaging.Converter.Serialize((System.Drawing.Icon)currentValue));
                                }
                                else
                                {
                                    XmlElement subElement = null;
                                    IEnumerable list = currentValue as IEnumerable;
                                    if ((list != null))
                                    {
                                        //CType(currentValue, IEnumerable)
                                        foreach (object item in list)
                                        {
                                            subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, item, type, xmlPropertiesInfo);
                                            if ((subElement != null))
                                                xmlNode.AppendChild(subElement);
                                        }
                                    }
                                    else
                                    {
                                        ICollection coll = currentValue as ICollection;
                                        if (coll != null)
                                        {
                                            foreach (object item in coll)
                                            {
                                                subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, item, type, xmlPropertiesInfo);
                                                if ((subElement != null))
                                                    xmlNode.AppendChild(subElement);
                                            }
                                        }
                                        else
                                        {
                                            subElement = CreateXmlElement(xmlDocument, propertyInfo.Name, currentValue, type, xmlPropertiesInfo);
                                            if ((subElement != null))
                                                xmlNode.AppendChild(subElement);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                xmlNode.SetAttribute(propertyInfo.Name, string.Empty);
                            }
                        }
                        catch (Exception ex)
                        {
                            ex = null;
                        }
                    }
                }
            }
            return xmlNode;
        }

        private static bool FindOverloadedProperty(PropertyInfo propertyInfo)
        {
            return false;
        }

        #endregion

        #region " ToXml Implementation "

        public static XmlDocument ToXmlDocument(object value)
        {
            return ToXmlDocument(null, value, Constants.Xml.UnicodeId);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding)
        {
            return ToXmlDocument(null, value, encoding);
        }

        public static XmlDocument ToXmlDocument(object value, params string[] propertyNames)
        {
            return ToXmlDocument(null, value, Constants.Xml.UnicodeId, propertyNames);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding, params string[] propertyNames)
        {
            return ToXmlDocument(null, value, encoding, propertyNames);
        }

        public static XmlDocument ToXmlDocument(object value, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, Constants.Xml.UnicodeId, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(object value, string encoding, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, encoding, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value)
        {
            return ToXmlDocument(previousXmlDocument, value, Constants.Xml.UnicodeId);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding)
        {
            return ToXmlDocument(previousXmlDocument, value, encoding);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, params string[] propertyNames)
        {
            return ToXmlDocument(previousXmlDocument, value, Constants.Xml.UnicodeId, propertyNames);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding, params string[] propertyNames)
        {
            Xml.XmlPropertiesInfo propertyInfo = new Xml.XmlPropertiesInfo(value.GetType(), propertyNames);
            return ToXmlDocument(previousXmlDocument, value, encoding, propertyInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(previousXmlDocument, value, Constants.Xml.UnicodeId, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(XmlDocument previousXmlDocument, object value, string encoding, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            XmlDocument xmlDocument = previousXmlDocument;
            System.Xml.XmlElement xmlNode = default(System.Xml.XmlElement);
            CheckPropertiesInfo(value.GetType(), propertiesInfo);
            try
            {
                if (xmlDocument == null)
                {
                    xmlDocument = new XmlDocument();
                    //Declaration
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Constants.Xml.Version, encoding, "yes"));
                }
                xmlNode = CreateXmlElement(xmlDocument, string.Empty, value, null, propertiesInfo);
                if ((xmlNode != null))
                {
                    if (previousXmlDocument == null)
                    {
                        xmlDocument.AppendChild(xmlNode);
                    }
                    else
                    {
                        xmlDocument.ChildNodes[1].AppendChild(xmlNode);
                    }
                }
            }
            finally
            {
            }
            return xmlDocument;
        }

        public static XmlDocument ToXmlDocument(string elementName, IQueryable collection, string encode, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(elementName, (IEnumerable)collection, encode, propertiesInfo);
        }

        public static XmlDocument ToXmlDocument(string elementName, IEnumerable collection, string encode, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            object member = null;
            XmlDocument xmlDocument = new XmlDocument();
            System.Xml.XmlElement xmlRootNode = default(System.Xml.XmlElement);
            string numberDecimalSeparator = ".";
            string numberGroupSeparator = ",";
            bool changeSeparator = false;
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.LCID);

            if (cultureInfo.NumberFormat.NumberDecimalSeparator != ".")
            {
                numberDecimalSeparator = cultureInfo.NumberFormat.NumberDecimalSeparator;
                numberGroupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                cultureInfo.NumberFormat.NumberGroupSeparator = ",";
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
                changeSeparator = true;
            }

            //Declaration
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Constants.Xml.Version, encode, "yes"));
            //Root
            if (string.IsNullOrEmpty(elementName))
            {
                string[] collectionName = collection.GetType().Name.Split('`');
                elementName = Convert.ToString((collectionName.Length > 0 ? collectionName[0] : collection.GetType().Name));
            }
            xmlRootNode = xmlDocument.CreateElement(elementName);
            xmlDocument.AppendChild(xmlRootNode);

            foreach (object member1 in collection)
            {
                Xml.XmlConverter.ToXmlDocument(xmlDocument, member1, encode, propertiesInfo);
            }

            if (changeSeparator)
            {
                cultureInfo.NumberFormat.NumberDecimalSeparator = numberDecimalSeparator;
                cultureInfo.NumberFormat.NumberGroupSeparator = numberGroupSeparator;
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            return xmlDocument;
        }

        //public static string ToXml<TValue>(TValue[] values)
        //{
        //    return SelectedItemSortedList.ToXml<TValue>(values);
        //}

        //public static string ToXml(object[] values)
        //{
        //    return SelectedItemSortedList.ToXml(values);
        //}       

        public static string ToXml(IEnumerable value, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument("IEnumerable", value, Constants.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(IQueryable value, params string[] propertyNames)
        {
            Xml.XmlPropertiesInfo propertiesInfo = new Xml.XmlPropertiesInfo(value.ElementType, propertyNames);
            return ToXmlDocument("IQueryable", value, Bc.Constants.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(IQueryable value, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument("IQueryable", value, Bc.Constants.Xml.UnicodeId, propertiesInfo).InnerXml;
        }

        public static string ToXml(object value)
        {
            return ToXml(value, (Bc.Xml.XmlPropertiesInfo[])null);
        }

        public static string ToXml(object value, string encoding)
        {
            return ToXml(value, encoding);
        }

        public static string ToXml(object value, params string[] propertyNames)
        {
            return ToXml(value, Constants.Xml.UnicodeId, propertyNames);
        }

        public static string ToXml(object value, string encoding, params string[] propertyNames)
        {
            Xml.XmlPropertiesInfo propertiesInfo = new Xml.XmlPropertiesInfo(value.GetType(), propertyNames);
            return ToXml(value, encoding, propertiesInfo);
        }

        public static string ToXml(object value, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXml(value, Constants.Xml.UnicodeId, propertiesInfo);
        }

        public static string ToXml(object value, string encoding, params Xml.XmlPropertiesInfo[] propertiesInfo)
        {
            return ToXmlDocument(null, value, encoding, propertiesInfo).InnerXml;
        }

        #endregion

        #region " LoadXml Implementation "

        public static void LoadXml(object value, XmlDocument xmlDocument)
        {
            try
            {
                if (xmlDocument.ChildNodes.Count > 0)
                {
                    foreach (XmlElement xmlElement in xmlDocument.ChildNodes)
                    {
                        if (value != null)
                        {
                            foreach (PropertyInfo propertyInfo in value.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase))
                            {
                                if (propertyInfo.CanWrite)
                                {
                                    XmlAttribute xmlAttribute = xmlElement.Attributes[propertyInfo.Name];
                                    if (xmlAttribute != null)
                                    {
                                        try
                                        {
                                            if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(bool)))
                                            {
                                                propertyInfo.SetValue(value, xmlAttribute.Value == "1", null);
                                            }
                                            else if (propertyInfo.PropertyType.IsEnum)
                                            {
                                                propertyInfo.SetValue(value, Enum.Parse(propertyInfo.PropertyType, xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(System.Drawing.Icon)))
                                            {
                                                propertyInfo.SetValue(value, Imaging.Converter.DeserializeToIcon(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(System.Drawing.Image)))
                                            {
                                                propertyInfo.SetValue(value, Imaging.Converter.DeserializeToImage(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(byte[])))
                                            {
                                                propertyInfo.SetValue(value, Imaging.Converter.DeserializeToBytes(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(int)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToInt32(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(string)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToString(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(short)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToInt16(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(byte)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToByte(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(decimal)))
                                            {
                                                propertyInfo.SetValue(value, Convert.ToDecimal(xmlAttribute.Value), null);
                                            }
                                            else if (object.ReferenceEquals(propertyInfo.PropertyType, typeof(DateTime)))
                                            {
                                                DateTime dateValue = new DateTime(Convert.ToInt32(xmlAttribute.Value.Substring(0, 4)), Convert.ToInt32(xmlAttribute.Value.Substring(4, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(6, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(9, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(12, 2)), Convert.ToInt32(xmlAttribute.Value.Substring(15, 2)));
                                                propertyInfo.SetValue(value, dateValue, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            ex = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        public static void LoadXml(object value, string xml)
        {
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xml);
            if ((xmlDocument.ChildNodes[0]) is XmlDeclaration)
                xmlDocument.RemoveChild(xmlDocument.ChildNodes[0]);
            LoadXml(value, xmlDocument);
        }

        #endregion

        #region " Replace special Xml marks "

        public static string UnFormat(string source)
        {
            string returnValue = string.Empty;
            if (object.ReferenceEquals(source.GetType(), typeof(string)))
            {
                returnValue = source.Replace(Constants.Format.Ampersand, "&");
                returnValue = source.Replace(Constants.Format.BeginTag, "<");
                returnValue = source.Replace(Constants.Format.EndTag, ">");
                returnValue = source.Replace(Constants.Format.Apostrophe, "'");
                returnValue = source.Replace(Constants.Format.QuotationMark, Constants.Format.Quotes);
            }
            return returnValue;
        }

        public static string Format(string source)
        {
            string returnValue = string.Empty;
            if (object.ReferenceEquals(source.GetType(), typeof(string)))
            {
                returnValue = source.Replace("&", Constants.Format.Ampersand);
                returnValue = source.Replace(Constants.Format.Quotes, Constants.Format.QuotationMark);
                returnValue = source.Replace("<", Constants.Format.BeginTag);
                returnValue = source.Replace(">", Constants.Format.EndTag);
                returnValue = source.Replace("'", Constants.Format.Apostrophe);
            }
            return returnValue;
        }
        #endregion

    }

    #endregion
}




namespace Bc.Imaging
{

    public sealed class Converter
    {

        #region " Capture Image "

        public static Image CaptureScreen()
        {
            return CaptureWindow(User32.GetDesktopWindow());
        }
        //CaptureScreen


        public static Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            User32.RECT windowRect = new User32.RECT();
            User32.GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;
            // create a device context we can copy to
            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
            // restore selection
            Gdi32.SelectObject(hdcDest, hOld);
            // clean up 
            Gdi32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);

            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            Gdi32.DeleteObject(hBitmap);

            return img;
        }
        //CaptureWindow


        public static void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }
        //CaptureWindowToFile


        public static void CaptureScreenToFile(string filename, ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }
        //CaptureScreenToFile

        private class Gdi32
        {

            public const int SRCCOPY = 0xcc0020;
            // BitBlt dwRop parameter
            [DllImport("gdi32.dll")]
            public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

            [DllImport("gdi32.dll")]
            public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteDC(IntPtr hDC);

            [DllImport("gdi32.dll")]
            public static extern bool DeleteObject(IntPtr hObject);

            [DllImport("gdi32.dll")]
            public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        }
        //GDI32

        private class User32
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
            //RECT

            [DllImport("user32.dll")]
            public static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowDC(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll")]
            public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        }
        //User32

        #endregion

        #region " Load Icons from File "

        [DllImport("shell32.dll")]
        private static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, ref int phiconLarge, ref int phiconSmall, int nIcons);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconExA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern int ExtractIconEx(string lpszFile, int nIconIndex, [In(), Out()]
int[] phIconLarge, [In(), Out()]
int[] phIconSmall, int nIcons);
        [DllImport("shell32.dll", EntryPoint = "ExtractIconA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int DestroyIcon(int hIcon);


        private static int GetNumberOfIcons(string fileName)
        {
            int i = 0;
            return ExtractIconEx(fileName, -1, ref i, ref i, 0);
        }

        public static System.Collections.Generic.List<Icon> GetIconsFromFile(string fileName)
        {
            System.Collections.Generic.List<Icon> returnValue = new System.Collections.Generic.List<Icon>();
            int indexIcon = 0;
            int numIcons = GetNumberOfIcons(fileName);
            int[] largeIcon = new int[numIcons];
            int[] smallIcon = new int[numIcons];
            ExtractIconEx(fileName, 0, largeIcon, smallIcon, numIcons);
            for (int index = 0; index <= numIcons - 1; index++)
            {
                Icon icon = Icon.FromHandle(new IntPtr(largeIcon[index]));
                returnValue.Add(icon);
                DestroyIcon(largeIcon[index]);
            }
            return returnValue;
        }

        private static System.Collections.Generic.List<Icon> GetIconsFromFile2(string fileName)
        {
            System.Collections.Generic.List<Icon> returnValue = new System.Collections.Generic.List<Icon>();
            int indexIcon = 0;
            Icon currentIcon = null;
            bool canRead = true;
            int iconLarge = 0;
            IntPtr handlerIcon = default(IntPtr);
            int iconSmall = 0;
            int numIcons = GetNumberOfIcons(fileName);
            if (!System.IO.File.Exists(fileName))
                return returnValue;

            try
            {
                for (int index = 0; index <= numIcons - 1; index++)
                {
                    handlerIcon = ExtractIcon(Process.GetCurrentProcess().Handle, fileName, index);
                    iconLarge = ExtractIcon(Process.GetCurrentProcess().Handle.ToInt32(), fileName, index);
                    if (iconLarge > 0)
                    {
                        System.IO.MemoryStream stream = new System.IO.MemoryStream();
                        Icon.FromHandle(handlerIcon).Save(stream);
                        stream.Position = 0;
                        returnValue.Add(new Icon(stream));
                        //Dim bytes(CInt(stream.Length)) As Byte
                        //stream.Position = 0
                        //stream.Read(bytes, 0, CInt(stream.Length))
                        //stream.Close()

                        //Dim strIcon As New IO.FileStream(String.Format("C:\Icons\{0}.ico", index + 1), FileMode.CreateNew)
                        //strIcon.Write(bytes, 0, bytes.Length)
                        //strIcon.Close()
                        //returnValue.Add(Icon.FromHandle(New IntPtr(iconLarge)))
                    }
                }

                //Do While canRead
                //    If ExtractIconEx(fileName, indexIcon, iconLarge, iconSmall, 1) > 0 Then
                //        currentIcon = Icon.FromHandle(New IntPtr(iconLarge))
                //    Else
                //        canRead = False
                //    End If
                //    'handlerIcon = ExtractIcon(Process.GetCurrentProcess().Handle, fileName, indexIcon)
                //    'If handlerIcon = IntPtr.Zero Then canRead = False
                //    If canRead Then
                //        'currentIcon = Icon.FromHandle(handlerIcon)
                //        If currentIcon Is Nothing Then Exit Do
                //        returnValue.Add(currentIcon)
                //        indexIcon += 1
                //    End If
                //Loop
            }
            catch (Exception ex)
            {
                returnValue.Clear();
            }
            return returnValue;
        }

        #endregion

        public static string ColorToHexadecimal(System.Drawing.Color color)
        {
            return System.Drawing.ColorTranslator.ToHtml(color);
            //Return String.Format("#{0}{1}{2}", Hex(color.R), Hex(color.G), Hex(color.B))
        }

        public static Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            if (sourceBMP == null)
                return null;

            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        public static Color HexadecimalToColor(string color)
        {
            if (color.StartsWith("#") & color.Length <= 7)
            {
                return System.Drawing.ColorTranslator.FromHtml(color);
            }
            else
            {
                return System.Drawing.Color.Transparent;
            }
        }

        public static System.Drawing.Icon ToIcon(System.Data.IDataReader reader, string fieldName)
        {
            byte[] bytes = { 0 };
            if (reader.IsDBNull(reader.GetOrdinal(fieldName)))
                return null;
            long size = reader.GetBytes(reader.GetOrdinal(fieldName), 0, null, 0, 0);
            System.Array.Clear(bytes, 0, bytes.GetLength(0));
            // ERROR: Not supported in C#: ReDimStatement

            bytes = new byte[size];

            reader.GetBytes(reader.GetOrdinal(fieldName), 0, bytes, 0, Convert.ToInt32(size));
            return ToIcon(bytes);
        }

        public static System.Drawing.Icon ToIcon(byte[] bytes, System.Drawing.Size size)
        {
            int count = 0;
            int total = 0;
            System.IO.MemoryStream stream = default(System.IO.MemoryStream);
            try
            {
                if (bytes.Length > 1)
                {
                    for (count = 0; count <= bytes.Length - 1; count++)
                    {
                        total = total + Convert.ToInt32(bytes.GetValue(count));
                    }
                    if (total > 0)
                    {
                        stream = new System.IO.MemoryStream(bytes);
                        System.Drawing.Icon iconReturn = new System.Drawing.Icon(stream, size);
                        return iconReturn;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static System.Drawing.Icon ToIcon(byte[] bytes, float width, float height)
        {
            return ToIcon(bytes, new System.Drawing.Size(Convert.ToInt32(width), Convert.ToInt32(height)));
        }

        public static System.Drawing.Icon ToIcon(byte[] bytes)
        {
            return ToIcon(bytes, new System.Drawing.Size(16, 16));
        }

        public static byte[] ToBytes(System.Drawing.Icon icon)
        {
            byte[] bytes = {
				
			};
            if (icon != null)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    icon.Save(stream);
                    Array.Resize<byte>(ref bytes, Convert.ToInt32(stream.Length));
                    stream.Position = 0;
                    stream.Read(bytes, 0, Convert.ToInt32(stream.Length));
                }
            }
            return bytes;
        }

        public static System.Drawing.Image ToImage(System.Data.IDataReader reader, string fieldName)
        {
            return ToImage(reader, fieldName, true);
        }

        public static System.Drawing.Image ToImage(System.Data.IDataReader reader, string fieldName, bool returnEmptyImageIfNull)
        {
            byte[] bytes = {
				
			};
            if (reader.IsDBNull(reader.GetOrdinal(fieldName)))
            {
                if (returnEmptyImageIfNull)
                {
                    return new System.Drawing.Bitmap(1, 1);
                }
                else
                {
                    return null;
                }
            }
            long size = reader.GetBytes(reader.GetOrdinal(fieldName), 0, null, 0, 0);
            System.Array.Clear(bytes, 0, bytes.GetLength(0));
            // ERROR: Not supported in C#: ReDimStatement

            bytes = new byte[size];
            reader.GetBytes(reader.GetOrdinal(fieldName), 0, bytes, 0, Convert.ToInt32(size));
            return ToImage(bytes);
        }

        public static System.Drawing.Image ToImage(byte[] bytes)
        {
            int count = 0;
            int total = 0;
            System.IO.MemoryStream stream = default(System.IO.MemoryStream);
            try
            {
                if (bytes.Length > 1)
                {
                    for (count = 0; count <= bytes.Length - 1; count++)
                    {
                        total += Convert.ToInt32(bytes.GetValue(count));
                    }
                    if (total > 0)
                    {
                        stream = new System.IO.MemoryStream(bytes);
                        return System.Drawing.Image.FromStream(stream, true);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] ToBytes(System.Drawing.Image image, System.Drawing.Imaging.ImageFormat imageFormat)
        {
            byte[] bytes = {
				
			};
            if (image != null)
            {
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    image.Save(stream, imageFormat);
                    Array.Resize<byte>(ref bytes, Convert.ToInt32(stream.Length));
                    stream.Position = 0;
                    stream.Read(bytes, 0, Convert.ToInt32(stream.Length));
                }
            }
            return bytes;
        }

        public static byte[] ToBytes(System.Drawing.Image image)
        {
            return ToBytes(image, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static string Serialize(System.Drawing.Icon icon)
        {
            return Serialize(ToBytes(icon));
        }

        public static string Serialize(System.Drawing.Image image)
        {
            return Serialize(ToBytes(image));
        }

        public static XmlDocument SerializeToXmlDocument(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            XmlTextWriter xmlWriter = new XmlTextWriter(writer);
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(bytes.GetType());
            XmlDocument xmlDocument = new XmlDocument();
            serializer.Serialize(xmlWriter, bytes);
            xmlWriter.Close();
            xmlDocument.LoadXml(builder.ToString());
            return xmlDocument;
        }

        public static string Serialize(byte[] bytes)
        {
            return SerializeToXmlDocument(bytes).ChildNodes[1].InnerText;
        }

        public static byte[] DeserializeToBytes(string value)
        {
            byte[] bytes = { 0 };
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode xmlNode = xmlDocument.CreateElement("base64Binary");
            xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration(Bc.Constants.Xml.Version, Bc.Constants.Xml.UnicodeId, string.Empty));
            xmlNode.InnerText = value;
            xmlDocument.AppendChild(xmlNode);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                xmlDocument.Save(stream);
                stream.Position = 0;
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(bytes.GetType());
                bytes = new byte[] { Convert.ToByte(serializer.Deserialize(stream)) };
            }
            return bytes;
        }

        public static System.Drawing.Image DeserializeToImage(string value)
        {
            return ToImage(DeserializeToBytes(value));
        }

        public static System.Drawing.Icon DeserializeToIcon(string value)
        {
            return ToIcon(DeserializeToBytes(value));
        }

        public static string SetImageFile(string fullPath)
        {
            string imageInfo = "";
            System.IO.FileInfo infoReader = default(System.IO.FileInfo);
            //infoReader = Computer.FileSystem.GetFileInfo(fullPath);
            if (infoReader == null)
                return "";
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            System.Xml.XmlElement xmlElement = default(System.Xml.XmlElement);
            xmlElement = xmlDocument.CreateElement("ImageInfo");
            xmlElement.SetAttribute("FileName", infoReader.Name);
            xmlElement.SetAttribute("Extension", infoReader.Extension);
            xmlElement.SetAttribute("Size", infoReader.Length.ToString());
            xmlElement.SetAttribute("DateModified", infoReader.LastWriteTime.ToShortDateString());
            xmlElement.SetAttribute("TimeModified", infoReader.LastWriteTime.ToShortTimeString());
            xmlElement.SetAttribute("Folder", infoReader.DirectoryName);
            return xmlElement.OuterXml;
        }

        public static string SetFormatImageFile(string innerXml)
        {
            string format = "";
            if (string.IsNullOrEmpty(innerXml))
                return "";
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(innerXml);
            foreach (System.Xml.XmlAttribute attribute in xmlDocument.FirstChild.Attributes)
            {
                format = format + attribute.Name + ": " + attribute.Value;// +Microsoft.VisualBasic.vbNewLine;
            }
            return format;
        }

        public static System.Drawing.Icon GetIconFromFile(string fileName)
        {
            return GetIconFromFile(fileName, new System.Drawing.Size(16, 16));
        }

        public static System.Drawing.Icon GetIconFromFile(string fileName, float width, float height)
        {
            return GetIconFromFile(fileName, new System.Drawing.Size(Convert.ToInt32(width), Convert.ToInt32(height)));
        }

        public static System.Drawing.Icon GetIconFromFile(string fileName, System.Drawing.Size size)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                return new System.Drawing.Icon(fileName, size);
            }
            else
            {
                return null;
            }
        }

        //Public Shared Function GetImageFromFile(ByVal fileName As String) As Drawing.Image
        //    Return GetImageFromFile(fileName, Drawing.Imaging.ImageFormat.Jpeg)
        //End Function

        public static System.Drawing.Image GetImageFromFile(string fileName)
        {
            //, ByVal imageFormat As Drawing.Imaging.ImageFormat) As Drawing.Image
            if (!string.IsNullOrEmpty(fileName))
            {
                return System.Drawing.Image.FromFile(fileName);
                //Dim image As Drawing.Image
                //Dim stream As New MemoryStream
                //Using (stream)
                //    Image = Drawing.Image.FromFile(fileName)
                //    Image.Save(stream, imageFormat)
                //    stream.Position = 0
                //    Return Drawing.Image.FromStream(stream)
                //End Using
            }
            else
            {
                return null;
            }
        }

        public static Bitmap GenerateScaledImage(int width, int height, byte[] imageData)
        {
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                Image oldImage = Image.FromStream(ms);

                // Do we return full scale image?

                if ((width == -1) || (height == -1))
                {
                    width = oldImage.Width;
                    height = oldImage.Height;
                }
                // Make adjustments to maintain aspect ratio

                else if (oldImage.Width > oldImage.Height)
                {
                    height = Convert.ToInt32(oldImage.Height * Decimal.Divide(width, oldImage.Width));
                }
                else
                {
                    width = Convert.ToInt32(oldImage.Width * Decimal.Divide(height, oldImage.Height));
                }

                // Create the resized image

                Bitmap bitmap = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(oldImage, 0, 0, width, height);
                    g.Dispose();
                }
                return bitmap;
            }
        }

        public static Image CombineImage(Image image1, Image image2)
        {
            return CombineImage(new Size(32, 32), image1, image2);
        }

        public static Image CombineImage(Size size, Image image1, Image image2)
        {
            return CombineImage(size, image1, new Point(0, 0), image2, new Point(0, 0), System.Drawing.Imaging.ImageFormat.Png);
        }

        public static Image CombineImage(Size size, Image image1, Image image2, bool useImagesSize)
        {
            return CombineImage(size, image1, new Point(0, 0), image2, new Point(0, 0), System.Drawing.Imaging.ImageFormat.Png, useImagesSize);
        }

        public static Image CombineImage(Size size, Image image1, Point position1, Image image2, Point position2, System.Drawing.Imaging.ImageFormat format)
        {
            return CombineImage(size, image1, position1, image2, position2, format, false);
        }

        public static Image CombineImage(Size size, Image image1, Point position1, Image image2, Point position2, System.Drawing.Imaging.ImageFormat format, bool useImagesSize)
        {
            Bitmap result = new Bitmap(size.Width, size.Height);
            Graphics graphic = Graphics.FromImage(result);
            graphic.Clear(Color.Transparent);
            if (useImagesSize)
            {
                graphic.DrawImage(image1, position1.X, position1.Y, image1.Width, image1.Height);
                graphic.DrawImage(image2, position2.X, position2.Y, image2.Width, image2.Height);
            }
            else
            {
                graphic.DrawImage(image1, position1);
                graphic.DrawImage(image2, position2);
            }
            using (MemoryStream stream = new MemoryStream())
            {
                result.Save(stream, format);
                return new Bitmap(stream, true);
            }
        }

        public static Icon CombineIcon(Icon icon1, Icon icon2)
        {
            return CombineIcon(Graphics.FromImage(icon1.ToBitmap()), icon1, icon2);
        }

        public static Icon CombineIcon(Graphics graphic, Icon icon1, Icon icon2)
        {
            return CombineIcon(graphic, new Size(32, 32), icon1, icon2);
        }

        public static Icon CombineIcon(Size size, Icon icon1, Icon icon2)
        {
            return CombineIcon(Graphics.FromImage(icon1.ToBitmap()), size, icon1, icon2);
        }

        public static Icon CombineIcon(Graphics graphic, Size size, Icon icon1, Icon icon2)
        {
            return CombineIcon(graphic, size, icon1, new Point(0, 0), icon2, new Point(0, 0));
        }

        public static Icon CombineIcon(Size size, Icon icon1, Point position1, Icon icon2, Point position2)
        {
            return CombineIcon(Graphics.FromImage(icon1.ToBitmap()), size, icon1, position1, icon2, position2);
        }

        public static Icon CombineIcon(Graphics graphic, Size size, Icon icon1, Point position1, Icon icon2, Point position2)
        {
            Icon result = new Icon(icon1, size.Width, size.Height);
            graphic.Clear(Color.Transparent);
            graphic.DrawIcon(icon1, position1.X, position1.Y);
            graphic.DrawIcon(icon2, position2.X, position2.Y);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                result.Save(stream);
                return new Icon(stream, size);
            }
        }

        public static Image GetDisabledImage(Image source)
        {
            Image disabled = (Image)source.Clone();
            
            // TODO: comentado para que funcione
            //System.Windows.Forms.ControlPaint.DrawImageDisabled(Graphics.FromImage(disabled), disabled, 0, 0, Color.Transparent);
            return new Bitmap(disabled);
        }

        public static string GetImageFormat(Image image)
        {
            string format = null;
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                format = "BMP";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
            {
                format = "EMF";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
            {
                format = "EXIF";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {
                format = "GIF";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
            {
                format = "Icon";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                format = "JPEG";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
            {
                format = "MemoryBMP";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                format = "PNG";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
            {
                format = "TIFF";
            }
            else if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Wmf))
            {
                format = "WMF";
            }

            return format;
        }

    }

}