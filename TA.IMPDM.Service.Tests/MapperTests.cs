using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TA.IMPDM.Service.Tests
{
    public class MapperTests
    {
        public MapperTests()
        {
            
        }

        [Fact]
        public void AllDestMembersHasSource()
        {
            var typeConfig = new TypeAdapterConfig();

            typeConfig.RequireDestinationMemberSource = true;
            MapperConfig.ConfigureMapper(typeConfig);

            //assert
            typeConfig.Compile();
        }
    }
}
