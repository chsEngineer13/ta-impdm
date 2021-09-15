using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace TA.IMPDM.Service
{
    public static class MapperConfig
    {
        public static void ConfigureMapper(TypeAdapterConfig config)
        {
            const string dateTimeOffsetFormat = "yyyy-MM-ddThh:mm:sszzz";
            const string dateTimeFormat = "yyyy-MM-dd";

            config.NewConfig<DB.Building, StreamObjects.StreamBuilding>()
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.HParentType, src => src.ParentType)
                .Map(dest => dest.HPidStr, src => src.ParentObjectId)
                .Map(dest => dest.HContractIdStr, src => src.ContractObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat))
                .Map(dest => dest.Number, src => src.Num);

            config.NewConfig<DB.Constrpart, StreamObjects.StreamConstrPart>()
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.HParentType, src => src.ParentType)
                .Map(dest => dest.HPidStr, src => src.ParentObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat))
                .Map(dest => dest.Number, src => src.Num);

            config.NewConfig<DB.Construction, StreamObjects.StreamConstruction>()
                .Map(dest => dest.Code, src => src.OipKs)
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat));

            config.NewConfig<DB.Contract, StreamObjects.StreamContract>()
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat))
                .Map(dest => dest.ContractDate, 
                     src => src.Cdate != null 
                        ? ((DateTime)src.Cdate).ToString(dateTimeFormat)
                        : null)
                .Map(dest => dest.Number, src => src.Num)
                .Map(dest => dest.CustomerCode, src => src.Ccode);

            config.NewConfig<DB.Docset, StreamObjects.StreamDocset>()
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.HParentType, src => src.ParentType)
                .Map(dest => dest.HPidStr, src => src.ParentObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat))
                .Map(dest => dest.DevDep, src => src.DevDep)
                .Map(dest => dest.CustomerCode, src => src.Ccode)
                .Map(dest => dest.ContractNumber, src => src.Num)
                .Map(dest => dest.CipherStage, src => src.Pstage)
                .Map(dest => dest.Developer, src => src.DevOrg)
                .Map(dest => dest.ConstrPartCode, src => src.Cpcode)
                .Map(dest => dest.ConstrPartNumber, src => src.Cpnum)
                .Map(dest => dest.BuildingCode, src => src.Bcode)
                .Map(dest => dest.BuildingNumber, src => src.Bnum)
                .Map(dest => dest.Stage, src => src.Bstage)
                .Map(dest => dest.ContractStage, src => src.Cstage)
                .Map(dest => dest.Changeset, src => src.IzmNum.ToString());

            config.NewConfig<DB.Document, StreamObjects.StreamDocument>()
                .Map(dest => dest.HidStr, src => src.ObjectId)
                .Map(dest => dest.HParentType, src => src.ParentType)
                .Map(dest => dest.HPidStr, src => src.ParentObjectId)
                .Map(dest => dest.TimeModified, src => src.ObjectTime.ToString(dateTimeOffsetFormat))
                .Map(dest => dest.HStatusStr, src => src.Status);

            config.Compile();
        }
    }
}
