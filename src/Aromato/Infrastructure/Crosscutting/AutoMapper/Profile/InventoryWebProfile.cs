using Aromato.Application.Web.Data;
using Aromato.Domain.Inventory;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class InventoryWebProfile : global::AutoMapper.Profile
    {
        public InventoryWebProfile()
        {
            var mapperRight = CreateMap<Inventory, InventoryWebData>();

            mapperRight.ForMember(d => d.Id, mc => mc.MapFrom(i => i.Id));
            mapperRight.ForMember(d => d.Name, mc => mc.MapFrom(i => i.Name));
            mapperRight.ForMember(d => d.Description, mc => mc.MapFrom(i => i.Description));
            mapperRight.ForMember(d => d.Items, mc => mc.MapFrom(i => i.Items.AsEnumerableData<ItemWebData>()));

            var mapperLeft = CreateMap<InventoryWebData, Inventory>();

//            mapperLeft.ForMember(e => e.Id, mc => mc.MapFrom(d => d.Id));
            mapperLeft.ForMember(e => e.Name, mc => mc.MapFrom(d => d.Name));
            mapperLeft.ForMember(e => e.Description, mc => mc.MapFrom(d => d.Description));
            mapperLeft.ForMember(e => e.Items, mc => mc.MapFrom(d => d.Items.AsEnumerableEntity<long, Item>()));
        }

    }
}