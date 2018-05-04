using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RRInventory.Models;

namespace RRInventory.Data
{
    public class DbInitializer
    {
        public static void Initialize(InventoryContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            Guid acct1 = Guid.NewGuid();
            Guid acct2 = Guid.NewGuid();

            var accounts = new Account[]
            {
            new Account{Name="Carson Wentz",ID=acct1, Address1="Dreamland Ave",City="Philadelphia", Zipcode="12345", SubscriptionEndDate=DateTime.Parse("2020-09-01")},
            new Account{Name="Doug Fletcher",ID=acct2, Address1="Milano Ct",City="Sanford", Zipcode="32771", SubscriptionEndDate=DateTime.Parse("2021-05-01")},
            };
            foreach (Account s in accounts)
            {
                context.Accounts.Add(s);
            }
            context.SaveChanges();

            Guid image1 = Guid.NewGuid();
            Guid image2 = Guid.NewGuid();
            Guid image3 = Guid.NewGuid();

            // Initialize Image Table?
            var images = new Image[]
            {
            new Image{ID=image1, Jpg="c:\\foo\\image1.jpg", Thumbnail="c:\\foo\\image1_thumb.jpg"},
            new Image{ID=image2, Jpg="c:\\foo\\image2.jpg", Thumbnail="c:\\foo\\image2_thumb.jpg"},
            new Image{ID=image3, Jpg="c:\\foo\\image3.jpg", Thumbnail="c:\\foo\\image3_thumb.jpg"},
            };

            foreach (Image i in images)
            {
                context.Images.Add(i);
            }

            context.SaveChanges();
            var items = new Item[]
            {
            new Item{ID=Guid.NewGuid(), AccountID=acct1, Description="Big Boy",Type = Item.ItemTypes.Locomotive, Railroad="Union Pacific", Manufacturer="Trix", ImageID=image1},
            new Item{ID=Guid.NewGuid(), AccountID=acct1, Description="Dining Car",Type = Item.ItemTypes.PassengerCar, Railroad="Union Pacific", Manufacturer="Walthers", ImageID=image2},
            new Item{ID=Guid.NewGuid(), AccountID=acct2, Description="Boxcar",Type = Item.ItemTypes.RollingStock, Railroad="Santa Fe", Manufacturer="Athearn", ImageID=image3},
            };
            foreach (Item i in items)
            {
                context.Items.Add(i);
            }
            context.SaveChanges();

        }
    }
}
