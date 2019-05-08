using System;
using System.ComponentModel;
using System.Reflection;

namespace FantasyManager.Core.Extensions
{
    public static class StringExtensions
    {
        public static string GetDescription( this Enum en )
        {
            FieldInfo fieldInfo = en.GetType().GetField( en.ToString() );

            var attributes =
                ( DescriptionAttribute[] ) fieldInfo.GetCustomAttributes( typeof( DescriptionAttribute ), false );

            if ( attributes != null && attributes.Length > 0 )
                return attributes[ 0 ].Description;
            else
                return en.ToString();

        }
    }
}
