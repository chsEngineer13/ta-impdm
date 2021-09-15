using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public class ODataServiceOptions
    {
        public IReadOnlyDictionary<Type, string> StreamTypeToODataTable { get; }

        public ODataServiceOptions(IReadOnlyDictionary<Type, string> streamTypeToODataTable)
        {
            StreamTypeToODataTable = streamTypeToODataTable ?? throw new ArgumentNullException(nameof(streamTypeToODataTable));
        }
    }
}
