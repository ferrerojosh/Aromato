using System;
using Aromato.Application.Web.Data;
using Aromato.Domain.Inventory;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class ItemWebProfile : global::AutoMapper.Profile
    {
        public ItemWebProfile()
        {
            var mapperRight = CreateMap<Item, ItemWebData>();

            mapperRight.ForMember(d => d.Id, mc => mc.MapFrom(i => i.Id));
            mapperRight.ForMember(d => d.UniqueId, mc => mc.MapFrom(i => i.UniqueId));
            mapperRight.ForMember(d => d.Name, mc => mc.MapFrom(i => i.Name));
            mapperRight.ForMember(d => d.Description, mc => mc.MapFrom(i => i.Description));
            mapperRight.ForMember(d => d.Status, mc => mc.MapFrom(i => i.Status.ToString()));
            mapperRight.ForMember(d => d.DateAdded, mc => mc.MapFrom(i => i.DateAdded.Value.ToString("O")));
            mapperRight.ForMember(d => d.LastUpdated, mc => mc.MapFrom(i => i.LastUpdated.Value.ToString("O")));

            var mapperLeft = CreateMap<ItemWebData, Item>();

//            mapperLeft.ForMember(e => e.Id, mc => mc.MapFrom(d => d.Id));
            mapperLeft.ForMember(e => e.UniqueId, mc => mc.MapFrom(d => d.UniqueId));
            mapperLeft.ForMember(e => e.Name, mc => mc.MapFrom(d => d.Name));
            mapperLeft.ForMember(e => e.Description, mc => mc.MapFrom(d => d.Description));
//            mapperLeft.ForMember(e => e.Status, mc => mc.MapFrom(d => Enum.Parse(typeof(ItemStatus), d.Status)));
//            mapperLeft.ForMember(e => e.DateAdded, mc => mc.MapFrom(d => DateTime.Parse(d.DateAdded)));
//            mapperLeft.ForMember(e => e.LastUpdated, mc => mc.MapFrom(d => DateTime.Parse(d.LastUpdated)));
        }
    }
}