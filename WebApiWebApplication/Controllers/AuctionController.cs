using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWebApplication.Models;

namespace WebApiWebApplication.Controllers {
    public class AuctionController : ApiController {

        private static readonly object Lock = new object();

        public IEnumerable<AuctionsItem> GetAllItems() {
            return Repo.Items;
        }

        public AuctionsItem GetItem(int ItemNumber) {

            var item =  Repo.Items.FirstOrDefault(x => x.ItemNumber == ItemNumber);

            if(item != null) {
                return item;
            } else {
                return null;
            }
        }

        public string Bid(Bid Bid) {
            var item = Repo.Items.FirstOrDefault(x => x.ItemNumber == Bid.ItemNumber);
            if(item != null) {
                if(Bid.Price > item.BidPrice) {
                    lock(Lock) {
                        item.BidPrice = Bid.Price;
                        item.BidCustomName = Bid.CustomName;
                        item.BidCustomPhone = Bid.CustomPhone;
                        item.BidTimestamp = DateTime.Now;
                    }
                    return "OK";
                } else {
                    return "Bud for lavt";
                }
            } else {
                return "Vare findes ikke";
            }
        }
    }
}
