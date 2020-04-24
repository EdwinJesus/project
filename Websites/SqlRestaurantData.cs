using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            Db = db;
        }

        public OdeToFoodDbContext Db { get; }

        public Restaurant Add(Restaurant restaurant)
        {
            Db.Add(restaurant);
            return restaurant;
        }

        public int Commit()
        {
            return Db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                Db.Restaurants.Remove(restaurant);
            }
           
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return Db.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return Db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in Db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;

            return query;
        }

        public Restaurant Update(Restaurant updateRestaurant)
        {
            var entity = Db.Restaurants.Attach(updateRestaurant);
            entity.State = EntityState.Modified;

            return updateRestaurant;
        }
    }
}
