using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Platform.Datas.Dtos;
using Tiger.Module.System.Platform.Datas;
using Tiger.Module.System.Platform.Layouts.Dto;
using Tiger.Module.System.Platform.Menus.Dto;
using Tiger.Module.System.Localization.Dtos;

namespace Tiger.Module.System.Localization
{
    public class LocalizationMappingProfile:Profile
    {
        public LocalizationMappingProfile()
        {
            
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageText, LanguageTextDto>();
            
        }
    }
}
