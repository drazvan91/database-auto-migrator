using System;
using System.Globalization;

namespace DatabaseAutoMigrator.Providers.Generic
{
    public class GenericDialect:IDialect
    {
        public virtual string ValueQuote { get { return "'";}}
        public virtual string EscapeValueQuote {  get { return "'";}}
        public virtual string OpenQuote { get { return "\""; } }
        public virtual string CloseQuote { get { return "\""; } }

        public virtual string QuoteColumnName(string name)
        {
            return string.Format("[{0}]", name);
        }
        public virtual string QuoteTableName(string name)
        {
            return string.Format("[{0}]", name);
        }
        public virtual string QuoteConstraintName(string name)
        {
            return string.Format("[{0}]", name);
        }
        public virtual string QuoteValue(object value)
        {
            if (value == null) { return this.Null; }

            string stringValue = value as string;
            if (stringValue != null) { return FormatValueString(stringValue); }

            if (value is char) { return FormatValueChar((char)value); }
            if (value is bool) { return FormatValueBool((bool)value); }
            if (value is Guid) { return FormatValueGuid((Guid)value); }
            if (value is DateTime) { return FormatValueDateTime((DateTime)value); }
            if (value.GetType().IsEnum) { return FormatValueEnum(value); }
            if (value is double) { return FormatValueDouble((double)value); }
            if (value is float) { return FormatValueFloat((float)value); }
            if (value is decimal) { return FormatValueDecimal((decimal)value); }
            if (value is byte[]) { return FormatValueByteArray((byte[])value); }

            return value.ToString();
        }

        #region value formats
        public virtual string FormatValueString(string value)
        {
            return ValueQuote + value.Replace(ValueQuote, EscapeValueQuote) + ValueQuote; 
        }
        public virtual string FormatValueChar(char value)
        {
            return ValueQuote + value + ValueQuote;
        }

        public virtual string FormatValueBool(bool value)
        {
            return (value) ? 1.ToString() : 0.ToString();
        }

        public virtual string FormatValueGuid(Guid value)
        {
            return ValueQuote + value.ToString() + ValueQuote;
        }

        public virtual string FormatValueDateTime(DateTime value)
        {
            return ValueQuote + (value).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture) + ValueQuote;
        }

        public virtual string FormatValueEnum(object value)
        {
            return ValueQuote + value.ToString() + ValueQuote;
        }
        protected virtual string FormatValueByteArray(byte[] value)
        {
            var hex = new System.Text.StringBuilder((value.Length * 2) + 2);
            hex.Append("0x");
            foreach (byte b in value)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private string FormatValueDecimal(decimal value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private string FormatValueFloat(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private string FormatValueDouble(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
        #endregion 

        public virtual string Null
        {
            get { return "NULL"; }
        }
        public virtual string NotNull
        {
            get { return "NOT NULL"; }
        }

        public virtual string Unique
        {
            get { return "UNIQUE"; }
        }
        public virtual string PrimaryKey
        {
            get { return "PRIMARY KEY"; }
        }
        public virtual string Identity
        {
            get { return "IDENTITY(1,1)"; }
        }
        public virtual string IfExists
        {
            get { return "IF EXISTS"; }
        }

        public virtual string Default(object defaultvalue)
        {
            if (defaultvalue == null) return null;
            return string.Format("DEFAULT {0}", this.QuoteValue(defaultvalue));
        }


        
    }
}
