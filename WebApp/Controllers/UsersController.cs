
using PagedList;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebService;
using WebService.Dto;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {

        IUserWebService _iuserService;


        public UsersController(IUserWebService iuserService)
        {
            _iuserService = iuserService;

        }


        #region UsersFunctions

        public ActionResult Users(string SearchString, string sortOrder, string CurrentFilter, int? Page)
        {
            ViewBag.CurrentSort = sortOrder;
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            ViewBag.CurrentFilter = SearchString;


            var b = _iuserService.GetAll();

            if (!String.IsNullOrEmpty(SearchString))
            {
                b = b.Where(s => s.FullName.Contains(SearchString)).ToList();
            }


            ViewBag.FullNameSortParm = sortOrder == "FullName" ? "fullname_desc" : "FullName";
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            //ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var students = from s in b
                           select s;
            switch (sortOrder)
            {
                case "id_desc":
                    students = students.OrderByDescending(s => s.UserId);
                    break;
                case "Name":
                    students = students.OrderBy(s => s.UserName);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.UserName);
                    break;
                case "FullName":
                    students = students.OrderBy(s => s.FullName);

                    break;
                case "fullname_desc":
                    students = students.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.CreationDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.CreationDate);
                    break;
                default:
                    students = students.OrderBy(s => s.UserId);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (Page ?? 1);

            ViewBag.Base = students.ToPagedList(pageNumber, pageSize);



            // ViewBag.Base = b;

            return View(students.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult EditUser(Guid UserId, string UserName)
        {
            ViewBag.UserId = UserId;
            ViewBag.UserName = UserName;
            var e = _iuserService.GetById(UserId);
            ViewBag.Email = e.Email;
            ViewBag.Date = e.CreationDate;
            ViewBag.Fullname = e.FullName;
            ViewBag.Phone = e.PhoneNumber;
            ViewBag.Role = e.Role;







            return View();
        }

        [HttpPost]
        public ActionResult EditUser(UserDto User)
        {
            var g = _iuserService.Edit(User);


            ViewBag.Base = g;




            return RedirectToAction("Users", g);

        }


        public ActionResult DeleteUser(Guid UserId)
        {
            //var ff = _iuserService.GetById(UserId);
            //SClassifyDto ss = new SClassifyDto();
            //ss.PublisherId = UserId;
            //var kk = _iclassifyService.Search(NawafizApp.Common.LanguageHelper.ARABIC, ss, UserId);
            //if (kk.Count == 0)
            //{


            //var g = _icityService.GetCityById(NawafizApp.Common.LanguageHelper.ARABIC, city);
            //foreach (var t in kk)
            //{
            //    if (t != null)
            //    {
            //        var c = DeleteTown(g.Id);
            //    }
            //}
            var b = _iuserService.Delete(UserId);


            ViewBag.Base = b;




            return RedirectToAction("UserDeleted");
        }




        public ActionResult UserNotDelete(UserDto g)
        {
            ViewBag.UserName = g.UserName;
            ViewBag.UserId = g.UserId;
            ViewBag.FullName = g.FullName;
            return View();

        }

        public ActionResult UserDeleted()
        {

            return View();

        }



        #endregion



        //public ActionResult AddClassifyToFavriteView(Guid UserId)
        //{
        //    var a = _iguidService.GetAll_for_Admin();
        //    //var a = _iCategoryService.GetAll_for_Admin();
        //    ViewBag.Categoriesss = a;
        //    List<SelectListItem> GuideItems = new List<SelectListItem>();
        //    foreach (var hh in a)
        //    {

        //        GuideItems.Add(new SelectListItem { Text = @hh.GuideArabicName, Value = @hh.Id.ToString() });
        //    }
        //    ViewData["GuideList"] = GuideItems;


        //    var Cities = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    //var Cities = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    ViewBag.Citiesss = Cities;
        //    List<SelectListItem> CityItems = new List<SelectListItem>();
        //    foreach (var hh in Cities)
        //    {

        //        CityItems.Add(new SelectListItem { Text = @hh.CityName, Value = @hh.Id.ToString() });
        //    }

        //    ViewData["CityList"] = CityItems;
        //    var Towns = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    //var Towns = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    ViewBag.Townsss = Towns;
        //    List<SelectListItem> TownItems = new List<SelectListItem>();
        //    foreach (var hh in Cities)
        //    {
        //        foreach (var kk in hh.TownsDto)
        //        { TownItems.Add(new SelectListItem { Text = @kk.TownName, Value = @kk.Id.ToString() }); }

        //    }
        //    ViewData["TownList"] = TownItems;



        //    ViewBag.Base = _iclassifyService.GetAll(NawafizApp.Common.LanguageHelper.ARABIC);
        //    return RedirectToAction("AddClassifyToFavorites");
        //}


        //public ActionResult AddClassifyToFavorites(Guid UserId, int Id)
        //{
        //    InputFavoriteDto dto = new InputFavoriteDto();
        //    dto.ClassifyId = Id;

        //    var g = _ifavoriteService.Add(dto, UserId);

        //    return RedirectToAction("FavoriteClassify", new { UserId = UserId });
        //}

        //public ActionResult DeleteClassifyFromFavorites(Guid UserId, int Id)
        //{
        //    var t = _ifavoriteService.Delete(Id, UserId);
        //    if (t == true)
        //        return RedirectToAction("FavoriteClassify", new { UserId = UserId });
        //    else
        //        return RedirectToAction("Classified", new { UserId = UserId });
        //}



        //public ActionResult FavoriteClassify(Guid UserId, string SearchString, string sortOrder, string CurrentFilter, int? Page, string MinPrice, string MaxPrice, string PreviousDate, string LaterDate, int? CityId, int? TownId, int? CategoryId)
        //{


        //    ViewBag.CurrentSort = sortOrder;
        //    if (SearchString != null)
        //    {
        //        Page = 1;
        //    }
        //    else
        //    {
        //        SearchString = CurrentFilter;
        //    }

        //    ViewBag.CurrentFilter = SearchString;
        //    ViewBag.MxPrice = MaxPrice;
        //    ViewBag.MnPrice = MinPrice;
        //    ViewBag.PreDate = PreviousDate;
        //    ViewBag.LateDate = LaterDate;
        //    ViewBag.TownId = TownId;
        //    ViewBag.CityId = CityId;
        //    ViewBag.UserId = UserId;
        //    ViewBag.CategoryId = CategoryId;


        //    var a = _iCategoryService.GetAll_for_Admin();
        //    ViewBag.Categoriesss = a;
        //    List<SelectListItem> CategoryItems = new List<SelectListItem>();
        //    foreach (var hh in a)
        //    {
        //        CategoryItems.Add(new SelectListItem { Text = @hh.CategoryArabicName, Value = @hh.Id.ToString() });
        //    }
        //    ViewData["CategoryList"] = CategoryItems;

        //    var Cities = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    ViewBag.Citiesss = Cities;
        //    List<SelectListItem> CityItems = new List<SelectListItem>();
        //    foreach (var hh in Cities)
        //    {
        //        CityItems.Add(new SelectListItem { Text = @hh.CityName, Value = @hh.Id.ToString() });
        //    }

        //    ViewData["CityList"] = CityItems;
        //    var Towns = _icityService.GetAllCities(NawafizApp.Common.LanguageHelper.ARABIC);
        //    ViewBag.Townsss = Towns;
        //    List<SelectListItem> TownItems = new List<SelectListItem>();
        //    foreach (var hh in Cities)
        //    {
        //        foreach (var kk in hh.TownsDto)
        //            TownItems.Add(new SelectListItem { Text = @kk.TownName, Value = @kk.Id.ToString() });
        //    }
        //    ViewData["TownList"] = TownItems;

        //    var ggg = _iclassifyService.GetAll(NawafizApp.Common.LanguageHelper.ARABIC);
        //    ViewBag.ggg = ggg;








        //    var b = _ifavoriteService.GetFavorites(NawafizApp.Common.LanguageHelper.ARABIC, UserId);
        //    List<ClassifyDetailedDto> b1 = new List<ClassifyDetailedDto>();
        //    // Guid fff = new Guid();
        //    foreach (var f in b)
        //    {
        //        b1.Add(_iclassifyService.GetDeatailedById(NawafizApp.Common.LanguageHelper.ARABIC, f.Id, UserId, null));
        //    }


        //    //b1[0].Id
        //    //    b1[0].CategoryId
        //    //    b1[0].Town
        //    List<CategoryDto> t = new List<CategoryDto>();
        //    List<TownDto> t1 = new List<TownDto>();
        //    ViewBag.b1 = b1;

        //    foreach (var k in b)
        //    {
        //        t.Add(_iCategoryService.GetById(NawafizApp.Common.LanguageHelper.ARABIC, k.CategoryId));

        //    }
        //    foreach (var g in b)
        //    {
        //        t1.Add(_itownService.GetTownById(NawafizApp.Common.LanguageHelper.ARABIC, g.TownId));

        //    }

        //    ViewBag.TownName = t1;
        //    ViewBag.CategoryName = t;


        //    if (!String.IsNullOrEmpty(SearchString))
        //    {
        //        b = b.Where(s => s.ClassifyName.Contains(SearchString)).ToList();
        //    }
        //    else if (!String.IsNullOrEmpty(MinPrice) || !String.IsNullOrEmpty(MaxPrice) || !String.IsNullOrEmpty(PreviousDate) || !String.IsNullOrEmpty(LaterDate) || !String.IsNullOrEmpty(SearchString) || TownId != null || CityId != null || CategoryId != null)
        //    {
        //        SClassifyDto ss = new SClassifyDto();
        //        if (!String.IsNullOrEmpty(LaterDate))
        //            ss.LaterDate = LaterDate;
        //        if (!String.IsNullOrEmpty(PreviousDate))
        //            ss.Date = PreviousDate;
        //        if (!String.IsNullOrEmpty(MinPrice))
        //            ss.Price = decimal.Parse(MinPrice);
        //        if (!String.IsNullOrEmpty(MaxPrice))
        //            ss.MaxPrice = decimal.Parse(MaxPrice);
        //        if (CityId != null) { ss.CityID = CityId; }
        //        if (TownId != null) { ss.TownID = TownId; }
        //        if (UserId != null) { ss.PublisherId = UserId; }
        //        if (CategoryId != null) { ss.CategoryID = CategoryId; }



        //        var tt = _iclassifyService.Search(NawafizApp.Common.LanguageHelper.ARABIC, ss, new Guid());

        //        b = tt;
        //        if (!String.IsNullOrEmpty(SearchString))
        //        {
        //            b = tt.Where(s => s.ClassifyName.Contains(SearchString)).ToList();
        //        }

        //    }

        //ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        //ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
        //ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
        //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //var students = from s in b
        //               select s;
        //switch (sortOrder)
        //{
        //    case "id_desc":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Id);
        //        break;
        //    case "Name":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.ClassifyName);
        //        break;
        //    case "name_desc":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.ClassifyName);
        //        break;
        //    case "Price":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.Price).OrderBy(s => s.Price.Count());

        //        break;
        //    case "price_desc":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Price).OrderByDescending(s => s.Price.Count());
        //        break;
        //    case "Date":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.Date);
        //        break;
        //    case "date_desc":
        //        students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Date);
        //        break;
        //    default:
        //        students = students.OrderBy(s => s.Id).OrderByDescending(s => s.IsFeatured);
        //        break;
        //}

        //    int pageSize = 3;
        //    int pageNumber = (Page ?? 1);

        //    ViewBag.Base = students.ToPagedList(pageNumber, pageSize);
        //    //ViewBag.Base = students;

        //    return View(students.ToPagedList(pageNumber, pageSize));





        //    // return View();
        //}



        //public ActionResult AddFavoriteClassify(Guid UserId, string SearchString, string sortOrder, string CurrentFilter, int? Page, string MinPrice, string MaxPrice, string PreviousDate, string LaterDate, int? CityId, int? TownId, int? CategoryId)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    if (SearchString != null)
        //    {
        //        Page = 1;
        //    }
        //    else
        //    {
        //        SearchString = CurrentFilter;
        //    }

        //    ViewBag.CurrentFilter = SearchString;
        //    ViewBag.MxPrice = MaxPrice;
        //    ViewBag.MnPrice = MinPrice;
        //    ViewBag.PreDate = PreviousDate;
        //    ViewBag.LateDate = LaterDate;
        //    ViewBag.TownId = TownId;
        //    ViewBag.CityId = CityId;
        //    ViewBag.UserId = UserId;
        //    ViewBag.CategoryId = CategoryId;



        //    var b = _iclassifyService.GetAll(Common.LanguageHelper.ARABIC);
        //    ViewBag.Base = b;

        //    List<CategoryDto> CategoryName = new List<CategoryDto>();
        //    foreach (var cc in b)
        //    {
        //        CategoryName.Add(_iCategoryService.GetById(Common.LanguageHelper.ARABIC, cc.CategoryId));
        //    }

        //    ViewBag.CategoryName = CategoryName;

        //    List<TownDto> TownName = new List<TownDto>();
        //    foreach (var cc in b)
        //    {
        //        TownName.Add(_itownService.GetTownById(Common.LanguageHelper.ARABIC, cc.TownId));
        //    }

        //    ViewBag.TownName = TownName;

        //    ViewBag.UserId = UserId;







        //    if (!String.IsNullOrEmpty(SearchString))
        //    {
        //        b = b.Where(s => s.ClassifyName.Contains(SearchString)).ToList();
        //    }
        //    else if (!String.IsNullOrEmpty(MinPrice) || !String.IsNullOrEmpty(MaxPrice) || !String.IsNullOrEmpty(PreviousDate) || !String.IsNullOrEmpty(LaterDate) || !String.IsNullOrEmpty(SearchString) || TownId != null || CityId != null || CategoryId != null)
        //    {
        //        SClassifyDto ss = new SClassifyDto();
        //        if (!String.IsNullOrEmpty(LaterDate))
        //            ss.LaterDate = LaterDate;
        //        if (!String.IsNullOrEmpty(PreviousDate))
        //            ss.Date = PreviousDate;
        //        if (!String.IsNullOrEmpty(MinPrice))
        //            ss.Price = decimal.Parse(MinPrice);
        //        if (!String.IsNullOrEmpty(MaxPrice))
        //            ss.MaxPrice = decimal.Parse(MaxPrice);
        //        if (CityId != null) { ss.CityID = CityId; }
        //        if (TownId != null) { ss.TownID = TownId; }
        //        if (UserId != null) { ss.PublisherId = UserId; }
        //        if (CategoryId != null) { ss.CategoryID = CategoryId; }



        //        var tt = _iclassifyService.Search(NawafizApp.Common.LanguageHelper.ARABIC, ss, new Guid());

        //        b = tt;
        //        if (!String.IsNullOrEmpty(SearchString))
        //        {
        //            b = tt.Where(s => s.ClassifyName.Contains(SearchString)).ToList();
        //        }

        //    }

        //    ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        //    ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
        //    ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    var students = from s in b
        //                   select s;
        //    switch (sortOrder)
        //    {
        //        case "id_desc":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Id);
        //            break;
        //        case "Name":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.ClassifyName);
        //            break;
        //        case "name_desc":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.ClassifyName);
        //            break;
        //        case "Price":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.Price).OrderBy(s => s.Price.Count());

        //            break;
        //        case "price_desc":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Price).OrderByDescending(s => s.Price.Count());
        //            break;
        //        case "Date":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderBy(s => s.Date);
        //            break;
        //        case "date_desc":
        //            students = students.OrderByDescending(s => s.IsFeatured).OrderByDescending(s => s.Date);
        //            break;
        //        default:
        //            students = students.OrderBy(s => s.Id).OrderByDescending(s => s.IsFeatured);
        //            break;
        //    }

        //    int pageSize = 3;
        //    int pageNumber = (Page ?? 1);

        //    ViewBag.Base = students.ToPagedList(pageNumber, pageSize);
        //    //ViewBag.Base = students;

        //    return View(students.ToPagedList(pageNumber, pageSize));



        //    // return View();

        //}





    }
}
