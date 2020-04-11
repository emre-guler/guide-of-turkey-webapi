using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GuideOfTurkey.Data;
using GuideOfTurkey.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt;
using Newtonsoft.Json;

namespace GuideOfTurkey.Controllers
{
    [Route("api/[controller]/useroperations")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly GuideContext db;
        public GuideController(GuideContext context)
        {
            db = context;
        }
        // domain/api/guide/useroperations
        [HttpGet]
        public string Get()
        {

            return "Guide Of Turkey uygulamasının omurgasına hoş geldiniz.";
        }
        // domain/api/guide/useroperations/register
        [HttpPost]
        [Route("register")]
        public string register([FromBody] registerClass register)
        {
            var phoneControl = db.userAccounts.Where(x => x.PhoneNumber == register.PhoneNumber).FirstOrDefault();
            if(phoneControl == null)
            {
                register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);
                UserAccount newUser = new UserAccount
                {
                    Fullname = register.Fullname,
                    PhoneNumber = register.PhoneNumber,
                    Password = register.Password,
                    photoUrl = "defaultpp.jpg",
                    userRank = false
                };
                db.userAccounts.Add(newUser);
                int saveControl = db.SaveChanges();
                if(saveControl == 1)
                {
                    return "success";
                } 
                else 
                {
                    return "saveControlPorblem";
                }
            }
            else 
            {
                return "sameNumber";
            }
        }
        // domain/api/guide/useroperations/login
        [HttpPost]
        [Route("login")]
        public string login([FromBody] loginClass login)
        {
            var userControl = db.userAccounts.Where(x => x.PhoneNumber == login.PhoneNumber).FirstOrDefault();
            if(userControl != null)
            {
                bool validPassword = BCrypt.Net.BCrypt.Verify(login.Password, userControl.Password);
                if(validPassword)
                {
                    string token = userControl.PhoneNumber + userControl.Fullname + userControl.userRank;
                    token = BCrypt.Net.BCrypt.HashPassword(token);
                    var commentCount = db.Comments.Where(x => x.UserID == userControl.ID).Count();
                    var favoriteCount = db.Favorites.Where(x => x.UserID == userControl.ID).Count();
                    Data newData = new Data
                    {
                        userID = userControl.ID,
                        FullName = userControl.Fullname,
                        PhoneNumber = userControl.PhoneNumber,
                        Token = token,
                        commentCount = commentCount,
                        favoriteCount = favoriteCount
                    };
                    string result = JsonConvert.SerializeObject(newData);
                    return result;
                }
                else
                {
                    return "wrongEntry";
                }
            }
            else 
            {
                return "wrongEntry";
            }
        }
        // domain/api/guide/useroperations/favorite
        [HttpPost]
        [Route("favorite")]
        public string favorite([FromBody] favoriteClass favorite)
        {
            var userForToken = db.userAccounts.Where(x => x.ID == favorite.UserID).FirstOrDefault();
            string token = userForToken.PhoneNumber + userForToken.Fullname + userForToken.userRank;
            bool validToken = BCrypt.Net.BCrypt.Verify(favorite.userToken, token);
            if(validToken)
            {
                if(favorite.favoriteType == "new")
                {
                    var data = db.Favorites.Where(x => x.UserID == favorite.UserID && x.PlaceID == favorite.PlaceID && x.deleteState == false).FirstOrDefault();
                    if(data == null)
                    {
                        Favorite newFavorite = new Favorite
                        {
                            PlaceID = favorite.PlaceID,
                            UserID = favorite.UserID
                        };
                        db.Favorites.Add(newFavorite);
                        int saveControl = db.SaveChanges();
                        if(saveControl == 1)
                        {
                            return "success";
                        }
                        else
                        {
                            return "saveControlProblem";
                        }
                    }
                    else
                    {
                        return "alreadyHave";
                    }                  
                }
                else
                {
                    var data = db.Favorites.Where(x => x.PlaceID == favorite.PlaceID && x.UserID == favorite.UserID).FirstOrDefault();
                    data.deleteState = true;
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else 
                    {
                        return "saveControlProblem";
                    }
                }
            }
            else
            {
                return "TokenError";
            }
        }
        // domain/api/guide/useroperations/comment
        [HttpPost]
        [Route("comment")]
        public string commentOperation([FromBody] commentClass comment)
        {
            var userForToken = db.userAccounts.Where(x => x.ID == comment.UserID).FirstOrDefault();
            string token = userForToken.PhoneNumber + userForToken.Fullname + userForToken.userRank;
            bool validToken = BCrypt.Net.BCrypt.Verify(token, comment.Token);
            if(validToken)
            {
                if(comment.commentType == "add")
                {
                    Comment newComment = new Comment
                    {
                        UserID = comment.UserID,
                        PlaceID = comment.PlaceID,
                        userComment = comment.userComment,
                        Rating = comment.Rating
                    };
                    db.Comments.Add(newComment);
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else
                    {
                        return "saveControlProblem";
                    }
                }
                else
                {
                    var data = db.Comments.Where(x => x.ID == comment.ID).FirstOrDefault();
                    data.deleteState = true;
                    int saveControl = db.SaveChanges();
                    if(saveControl == 1)
                    {
                        return "success";
                    }
                    else 
                    {
                        return "saveControlProblem";
                    }
                }
            }
            else
            {
                return "TokenError";
            }
        }
    }
    [Route("api/[controller]/data")]
    [ApiController]
    public class GuideDataController : ControllerBase
    {
        private readonly GuideContext db;
        public GuideDataController(GuideContext context)
        {
            db = context;
        }
        // domain/api/guidedata/data/homepageslider
        [HttpPost]
        [Route("homepageslider")]
        public string slider()
        {
            var data = db.Photos.Where(x => x.sliderState == true && x.deleteState == false).ToList();
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        // domain/api/guidedata/data/homepagetype
        [HttpPost]
        [Route("homepagetype")]
        public string type()
        {
            var data = db.Types.Where(x => x.homepageState == true && x.deleteState == false).ToList();
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        [HttpPost]
        [Route("typeContent")]
        public string typeContent([FromBody] typeContentClass typeContent)
        {
            var data = db.Places.Where(x => x.deleteState == false && x.TypeID == typeContent.id).ToList();
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        // domain/api/guidedata/data/homepagecity
        [HttpPost]
        [Route("homepagecity")]
        public string city()
        {
            var data = db.Cities.Where(x => x.deleteState == false && x.homepageState == true).ToList();
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
        // domain/api/guidedata/data/favorite
        [HttpPost]
        [Route("favorite")]
        public string favorite([FromBody] favoriteClass favorite)
        {
            var userForToken = db.userAccounts.Where(x => x.ID == favorite.UserID).FirstOrDefault();
            string token = userForToken.PhoneNumber + userForToken.Fullname + userForToken.userRank;
            bool validToken = BCrypt.Net.BCrypt.Verify(token, favorite.userToken);
            if(validToken)
            {
                var data = db.Favorites.Where(x => x.deleteState == false && x.UserID == favorite.UserID).ToList();
                int dataCount = data.Count();
                string[] places = new string[dataCount];
                int counter = 0;
                foreach(var i in data)
                {
                    var place = db.Places.Where(x => x.ID == i.ID).FirstOrDefault();
                    places[counter] = db.Districts.Where(x => x.ID == place.DistrictID).FirstOrDefault().Name + " / " + place.Name;
                    counter++;
                }
                string json = JsonConvert.SerializeObject(places);
                return json;
            }
            else 
            {
                return "TokenError";
            }
        }
        [HttpPost]
        [Route("contentpage")]
        public string contentpage([FromBody] contentDataClass content)
        {
            if(content.ContentType == "Country")
            {
                var countryData = db.Countries.Where(x => x.deleteState == false && x.ID == content.ContentID).Select(x => new{ Type = "Country", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).FirstOrDefault();
                string json = JsonConvert.SerializeObject(countryData);
                return json;
            }
            else if(content.ContentType == "City")
            {
                var cityData = db.Cities.Where(x => x.deleteState == false && x.ID == content.ContentID).Select(x => new { Type = "City", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).FirstOrDefault();
                string json = JsonConvert.SerializeObject(cityData);
                return json;
            }
            else if(content.ContentType == "District")
            {
                var districtData = db.Districts.Where(x => x.deleteState == false && x.ID == content.ContentID).Select(x => new { Type = "District", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).FirstOrDefault();
                string json = JsonConvert.SerializeObject(districtData);
                return json;
            }
            else if(content.ContentType == "Place")
            {
                var placeData = db.Places.Where(x => x.deleteState == false && x.ID == content.ContentID).Select(x => new { Type = "Place", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl, Rating = x.Rating}).FirstOrDefault();
                string json = JsonConvert.SerializeObject(placeData);
                return json;
            }
            else
            {
                return "somethingwentworng";
            }
        }
        [HttpPost]
        [Route("placetovisit")]
        public string contentPagePlacetovisit([FromBody] contentDataClass content)
        {
            var place = db.Places.Where(x => x.deleteState == false && x.Rating > 4).ToList();
            string json = JsonConvert.SerializeObject(place);
            return json;
        }
        [HttpPost]
        [Route("placephotos")]
        public string contentPagePhoto([FromBody] contentDataClass data)
        {

            if(data.ContentType == "Country")
            {
                var result = db.CountryGalleries.Where(x => x.deleteState == false && x.CountryID == data.ContentID).ToList();
                string json = JsonConvert.SerializeObject(result);
                if(json == "[]")
                {
                    return "";
                }
                return json;
            }
            else if(data.ContentType == "City")
            {
                var result = db.CityGalleries.Where(x => x.deleteState == false && x.CityID == data.ContentID).ToList();
                string json = JsonConvert.SerializeObject(result);
                if(json == "[]")
                {
                    return "";
                }
                return json;
            }
            else if(data.ContentType == "District")
            {
                var result = db.DistrictGalleries.Where(x => x.deleteState == false && x.districtID == data.ContentID).ToList();
                string json = JsonConvert.SerializeObject(result);
                if(json == "[]")
                {
                    return "";
                }
                return json;
            }
            else if(data.ContentType == "Place")
            {
                var result = db.PlaceGalleries.Where(x => x.deleteState == false && x.PlaceId == data.ContentID).ToList();
                string json = JsonConvert.SerializeObject(result);
                if(json == "[]")
                {
                    return "";
                }
                return json;
            }
            else
            {
                return "somethingwentworng";
            }
        }
        [HttpPost]
        [Route("placecomments")]
        public string showComments([FromBody] showCommentClass comment)
        {
            if(comment.Type == "oneComment")
            {
                var commentData = db.Comments.Where(x => x.deleteState == false && x.PlaceID == comment.PlaceID).FirstOrDefault();
                var userData = db.userAccounts.Where(x => x.deleteState == false && x.ID == commentData.ID).FirstOrDefault();
                string jsonComment = JsonConvert.SerializeObject(commentData);
                string jsonUser = JsonConvert.SerializeObject(userData);
                string[] array = new string[] { jsonComment, jsonUser };
                string json = JsonConvert.SerializeObject(array);
                return json;
            }
            else 
            {
                return "somethingwentwrong";
            }
        }
        [HttpPost]
        [Route("search")]
        public string search([FromBody] searchTextDataClass search )
        {
            if(search.searchType == "place")
            {
                var searchData = db.Places.Where(x => x.Name.Contains(search.searchText)).ToList();
                string json = JsonConvert.SerializeObject(searchData);
                return json;
            }
            else if (search.searchType == "city")
            {
                var searchData = db.Cities.Where(x => x.Name.Contains(search.searchText)).ToList();
                string json = JsonConvert.SerializeObject(searchData);
                return json;
            }
            else if (search.searchType == "generalSearch")
            {
                // ülkeler, şehirler, ilçeler, alanlar
                var countries = db.Countries.Where(x => x.deleteState == false && x.Name.Contains(search.searchText)).Select(x => new { Type = "Country", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).ToList();
                string jsonCountries = JsonConvert.SerializeObject(countries);

                var cities = db.Cities.Where(x => x.deleteState == false && x.Name.Contains(search.searchText)).Select(x => new { Type = "City", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).ToList();
                string jsonCities = JsonConvert.SerializeObject(cities);

                var districts = db.Districts.Where(x => x.deleteState == false && x.Name.Contains(search.searchText)).Select(x => new { Type = "District", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl}).ToList();
                string jsonDistricts = JsonConvert.SerializeObject(districts);

                var places = db.Places.Where(x => x.deleteState == false && x.Name.Contains(x.Name)).Select(x => new { Type = "Place", ID = x.ID, Name = x.Name, Explain = x.Explain, PhotoUrl = x.photoUrl, Rating = x.Rating}).ToList();
                string jsonPlaces = JsonConvert.SerializeObject(places);

                string[] data = new string[] { jsonCountries, jsonCities, jsonDistricts, jsonPlaces };
                string result = JsonConvert.SerializeObject(data);
                return result;
            }
            else
            {
                return "wrongParameter";   
            }
        }
    }
    public class registerClass 
    {
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
    public class loginClass
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
    public class Data 
    {
        public int userID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public int commentCount { get; set; }
        public int favoriteCount { get; set; }
    }
    public class favoriteClass
    {
        public int UserID { get; set; }
        public int PlaceID { get; set; }
        public string userToken { get; set; }
        public string favoriteType { get; set; }
    }
    public class commentClass 
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int PlaceID { get; set; }
        public string Token  { get; set; }
        public string userComment { get; set; }
        public float Rating { get; set; }
        public string commentType { get; set; }
    }
    public class profileDataClass
    {
        public int UserID { get; set; }
        public string userToken { get; set; }
    }
    public class contentDataClass
    {
        public int ContentID { get; set; }
        public string ContentType { get; set; }
    }
    public class searchTextDataClass
    {
        public string searchText { get; set; }
        public string searchType { get; set; }
    }
    public class typeContentClass 
    {
        public int id { get; set; }
    }
    public class showCommentClass
    {
        public int PlaceID { get; set; }
        public string Type { get; set; }
    }
}