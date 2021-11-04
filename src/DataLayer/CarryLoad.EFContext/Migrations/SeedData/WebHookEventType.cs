using CarryLoad.EFContext.Extensions;
using CarryLoad.Models;
using CarryLoad.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarryLoad.EFContext.Migrations.SeedData
{
    public class DefaultWebHookEventTypeCreator
    {
        private readonly ModelBuilder _modelBuilder;

        public DefaultWebHookEventTypeCreator(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var (key, value) in EnumExtensions.EnumToList<Enums.WebhookEventTypes>())
            {
                _modelBuilder.Entity<WebHookEventType>()
                    .HasData(
                        new WebHookEventType
                        {
                            Id = (int)key,
                            Name = key.ToString()
                        }
                    );
            }
        }
    }
}
