using System;
using UnityEngine;

public static class SocString
{
    public static CString Format<TA>(string InFormatStr, TA arg1)
        where TA : IConvertible
    {
		return Format<TA, int, int, int>( InFormatStr, arg1, 0, 0, 0, 1);
    }

	public static CString Format<TA, TB>(string InFormatStr, TA arg1, TB arg2 )
        where TA : IConvertible
        where TB : IConvertible
    {
        return Format<TA, TB, int, int>(InFormatStr, arg1, arg2, 0, 0, 2);
    }

	public static CString Format<TA, TB, TC>(string InFormatStr, TA arg1, TB arg2, TC arg3 )
        where TA : IConvertible
        where TB : IConvertible
        where TC : IConvertible
    {
		return Format<TA, TB, TC, int>( InFormatStr, arg1, arg2, arg3, 0,3);
    }
    
    public static CString Format<TA, TB, TC, TD>(string InFormatStr, TA arg1, TB arg2, TC arg3, TD arg4)
        where TA : IConvertible
        where TB : IConvertible
        where TC : IConvertible
        where TD : IConvertible
    {
        return Format<TA, TB, TC, TD>( InFormatStr, arg1, arg2, arg3, arg4,4);
    }


	//! Concatenate a formatted string with arguments
    public static CString Format<TA,TB,TC,TD>(string InFormatStr, TA arg1, TB arg2, TC arg3, TD arg4, int InArgCount)
        where TA : IConvertible
        where TB : IConvertible
        where TC : IConvertible
        where TD : IConvertible
    {
        using (CString.Block())
        {
            CString sb = CString.Alloc(InFormatStr.Length + 256);
            
            int verbatim_range_start = 0;

		    for ( int index = 0; index < InFormatStr.Length; index++ )
            {
			    if ( InFormatStr[index] == '{' )
                {
				    // Formatting bit now, so make sure the last block of the string is written out verbatim.
				    if ( verbatim_range_start < index )
				    {
					    // Write out unformatted string portion
                        sb.Append(InFormatStr, verbatim_range_start, index - verbatim_range_start);
				    }

                    uint base_value = 10;
                    uint padding = 0;
                    uint decimal_places = 5; // Default decimal places in .NET libs

                    index++;
                    char format_char = InFormatStr[index];
                    if ( format_char == '{' )
                    {
                        sb.Append('{');
                        index++;
                    }
                    else
                    {
                        index++;

                        if ( InFormatStr[index] == ':' )
                        {
                            // Extra formatting. This is a crude first pass proof-of-concept. It's not meant to cover
                            // comprehensively what the .NET standard library Format() can do.
                            index++;

                            // Deal with padding
                            while ( InFormatStr[index] == '0' )
                            {
                                index++;
                                padding++;
                            }
                            
                            if ( InFormatStr[index] == 'X' )
                            {
                                index++;

                                // Print in hex
                                base_value = 16;

                                // Specify amount of padding ( "{0:X8}" for example pads hex to eight characters
                                if ( ( InFormatStr[index] >= '0' ) && ( InFormatStr[index] <= '9' ) )
                                {
                                    padding = (uint)( InFormatStr[index] - '0' );
                                    index++;
                                }
                            }
                            else if ( InFormatStr[index] == '.' )
                            {
                                index++;

                                // Specify number of decimal places
                                decimal_places = 0;

                                while ( InFormatStr[index] == '0' )
                                {
                                    index++;
                                    decimal_places++;
                                }
                            }        
                        }
                       

                        // Scan through to end bracket
                        while ( InFormatStr[index] != '}' )
                        {
                            index++;
                        }

                        // Have any extended settings now, so just print out the particular argument they wanted
                        switch ( format_char )
                        {
                            case '0': sb.FormatValue<TA>( arg1, padding, base_value, decimal_places ); break;
                            case '1': sb.FormatValue<TB>( arg2, padding, base_value, decimal_places ); break;
                            case '2': sb.FormatValue<TC>( arg3, padding, base_value, decimal_places ); break;
                            case '3': sb.FormatValue<TD>( arg4, padding, base_value, decimal_places ); break;
                            default:
                                Debug.Assert(false, "Invalid parameter index"); break;
                        }
                    }

				    // Update the verbatim range, start of a new section now
				    verbatim_range_start = ( index + 1 );
                }
		    }

		    // Anything verbatim to write out?
		    if ( verbatim_range_start < InFormatStr.Length )
		    {
			    // Write out unformatted string portion
                sb.Append(InFormatStr, verbatim_range_start, InFormatStr.Length - verbatim_range_start);
		    }

            return sb;
        }
    }        
    
    private static void FormatValue<T>(this CString InSb, T arg, uint padding, uint base_value, uint decimal_places ) where T : IConvertible
    {
        switch ( arg.GetTypeCode() )
        {
            case System.TypeCode.UInt32:
            {
                InSb.Append( arg.ToUInt32( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.Int32:
            {
                InSb.Append( arg.ToInt32( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.Single:
            {
                InSb.Append( arg.ToSingle( System.Globalization.NumberFormatInfo.CurrentInfo));
                break;
            }

            case System.TypeCode.String:
            {
                InSb.Append( Convert.ToString( arg ) );
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
