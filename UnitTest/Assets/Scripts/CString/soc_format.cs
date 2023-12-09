using System;
using UnityEngine;

public static class SocString
{
        //! Concatenate a formatted string with arguments
	public static string Format<A>(String format_string, A arg1)
        where A : IConvertible
    {
		//return string_builder.ConcatFormat<A, int, int, int>( format_string, arg1, 0, 0, 0 );
        return String.Empty;
    }

	//! Concatenate a formatted string with arguments
	public static string Format<A, B>(String format_string, A arg1, B arg2 )
        where A : IConvertible
        where B : IConvertible
    {
		//return string_builder.ConcatFormat<A, B, int, int>( format_string, arg1, arg2, 0, 0 );
        return String.Empty;
    }

	//! Concatenate a formatted string with arguments
	public static string Format<A, B, C>( this CString str, String format_string, A arg1, B arg2, C arg3 )
        where A : IConvertible
        where B : IConvertible
        where C : IConvertible
    {
		//return string_builder.ConcatFormat<A, B, C, int>( format_string, arg1, arg2, arg3, 0 );
        return String.Empty;
    }

	//! Concatenate a formatted string with arguments
    public static string Format<A,B,C,D>(String format_string, A arg1, B arg2, C arg3, D arg4 )
        where A : IConvertible
        where B : IConvertible
        where C : IConvertible
        where D : IConvertible
    {
		int verbatim_range_start = 0;

		for ( int index = 0; index < format_string.Length; index++ )
        {
			if ( format_string[index] == '{' )
            {
				// Formatting bit now, so make sure the last block of the string is written out verbatim.
				if ( verbatim_range_start < index )
				{
					// Write out unformatted string portion
					//string_builder.Append( format_string, verbatim_range_start, index - verbatim_range_start );
				}

                uint base_value = 10;
                uint padding = 0;
                uint decimal_places = 5; // Default decimal places in .NET libs

                index++;
                char format_char = format_string[index];
                if ( format_char == '{' )
                {
                    //string_builder.Append( '{' );
                    index++;
                }
                else
                {
                    index++;

                    if ( format_string[index] == ':' )
                    {
                        // Extra formatting. This is a crude first pass proof-of-concept. It's not meant to cover
                        // comprehensively what the .NET standard library Format() can do.
                        index++;

                        // Deal with padding
                        while ( format_string[index] == '0' )
                        {
                            index++;
                            padding++;
                        }
                        
                        if ( format_string[index] == 'X' )
                        {
                            index++;

                            // Print in hex
                            base_value = 16;

                            // Specify amount of padding ( "{0:X8}" for example pads hex to eight characters
                            if ( ( format_string[index] >= '0' ) && ( format_string[index] <= '9' ) )
                            {
                                padding = (uint)( format_string[index] - '0' );
                                index++;
                            }
                        }
                        else if ( format_string[index] == '.' )
                        {
                            index++;

                            // Specify number of decimal places
                            decimal_places = 0;

                            while ( format_string[index] == '0' )
                            {
                                index++;
                                decimal_places++;
                            }
                        }        
                    }
                   

                    // Scan through to end bracket
                    while ( format_string[index] != '}' )
                    {
                        index++;
                    }

                    // Have any extended settings now, so just print out the particular argument they wanted
                    switch ( format_char )
                    {
                        case '0': FormatValue<A>( arg1, padding, base_value, decimal_places ); break;
                        case '1': FormatValue<B>( arg2, padding, base_value, decimal_places ); break;
                        case '2': FormatValue<C>( arg3, padding, base_value, decimal_places ); break;
                        case '3': FormatValue<D>( arg4, padding, base_value, decimal_places ); break;
                        default:
                            Debug.Assert(false, "Invalid parameter index"); break;
                    }
                }

				// Update the verbatim range, start of a new section now
				verbatim_range_start = ( index + 1 );
            }
		}

		// Anything verbatim to write out?
		//if ( verbatim_range_start < format_string.Length )
		{
			// Write out unformatted string portion
		//	string_builder.Append( format_string, verbatim_range_start, format_string.Length - verbatim_range_start );
		}

        //return string_builder;
        return String.Empty;
    }        
    
    private static void FormatValue<T>(T arg, uint padding, uint base_value, uint decimal_places ) where T : IConvertible
    {
        CString string_builder = CString.Alloc( 32 );
        
        switch ( arg.GetTypeCode() )
        {
            case System.TypeCode.UInt32:
            {
                string_builder.Append( arg.ToUInt32( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.Int32:
            {
                string_builder.Append( arg.ToInt32( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.Single:
            {
                string_builder.Append( arg.ToSingle( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.String:
            {
                string_builder.Append( Convert.ToString( arg ) );
                break;
            }

            default:
            {
                Debug.Assert( false, "Unknown parameter type" );
                break;
            }
        }
    }
}
