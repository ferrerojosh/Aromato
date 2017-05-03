using Aromato.Application.Web.Data;
using Aromato.Domain.Inventory;
using Aromato.Infrastructure.Crosscutting.Extension;

namespace Aromato.Infrastructure.Crosscutting.AutoMapper.Profile
{
    public class InventoryWebProfile : global::AutoMapper.Profile
    {
        public InventoryWebProfile()
        {
            var mapper = CreateMap<Inventory, InventoryWebData>();

            mapper.ForMember(d => d.Id, mc => mc.MapFrom(i => i.Id));
            mapper.ForMember(d => d.Name, mc => mc.MapFrom(i => i.Name));
            mapper.ForMember(d => d.Description, mc => mc.MapFrom(i => i.Description));
            mapper.ForMember(d => d.Items, mc => mc.MapFrom(i => i.Items.AsEnumerableData<ItemWebData>()));

            var itemMapper = CreateMap<Item, ItemWebData>();

            itemMapper.ForMember(d => d.Id, mc => mc.MapFrom(i => i.Id));
            itemMapper.ForMember(d => d.UniqueId, mc => mc.MapFrom(i => i.UniqueId));
            itemMapper.ForMember(d => d.Name, mc => mc.MapFrom(i => i.Name));
            itemMapper.ForMember(d => d.Description, mc => mc.MapFrom(i => i.Description));
            itemMapper.ForMember(d => d.Status, mc => mc.MapFrom(i => i.Status.ToString()));
            itemMapper.ForMember(d => d.DateAdded, mc => mc.MapFrom(i => i.DateAdded.Value.ToString("O")));
            itemMapper.ForMember(d => d.LastUpdated, mc => mc.MapFrom(i => i.LastUpdated.Value.ToString("O")));
        }

    }
}