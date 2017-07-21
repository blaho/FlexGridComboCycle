using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace FlexGridComboCycle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            c1FlexGrid1.DataSource = new BindingSource() { DataSource = new List<MyClass> { new MyClass() } };
            c1FlexGrid1.Cols[nameof(MyClass.LocalizedComboProperty)].ComboList = "Item A|Item B|Item C";  //  <------------------------------------
        }  //                                                                                                                                     |
    }  //                                                                                                                                         |
    //                                                                                                                                            |
    public class MyClass  //                                                                                                                      |
    {   //                                                                                                                                        |
        public RegularEnum RegularProperty { get; set; } // regular property cycles fine                                                          |
        public LocalizedEnum LocalizedProperty { get; set; } // localized property works, if combolist is not localized                           |
        public LocalizedEnum LocalizedComboProperty { get; set; } // localized property doesn't work, if combolist items are localized as well ----
    }


    public enum RegularEnum
    {
        ItemA,
        ItemB,
        ItemC
    }

    [TypeConverter(typeof(LocalizedEnumTypeConverter))]
    public enum LocalizedEnum
    {
        ItemA,
        ItemB,
        ItemC
    }

    public class LocalizedEnumTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string valueStr)
                switch (valueStr)
                {
                    case "Item A": return LocalizedEnum.ItemA;
                    case "Item B": return LocalizedEnum.ItemB;
                    case "Item C": return LocalizedEnum.ItemC;
                }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                switch ((LocalizedEnum)value)
                {
                    case LocalizedEnum.ItemA: return "Item A";
                    case LocalizedEnum.ItemB: return "Item B";
                    case LocalizedEnum.ItemC: return "Item C";
                }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
