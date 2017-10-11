using System.Linq;
using BallingMaskiner.Models;

namespace BallingMaskiner.Services
{
    public class SearchService
    {
        private readonly IntraDbContext _db = new IntraDbContext();

        public SearchResultViewModel Search(string searchValue, string category)
        {
            SearchResultViewModel searchResult = new SearchResultViewModel();

            switch (category)
            {
                case "Maskiner":
                    searchResult = FindMachines(searchResult, searchValue);
                    break;
                case "Kontakter":
                    searchResult = FindContacts(searchResult, searchValue);
                    break;
                case "Manualer":
                    searchResult = FindManuals(searchResult, searchValue);
                    break;
                case "Prospekter":
                    searchResult = FindProspects(searchResult, searchValue);
                    break;
                case "Tilbud":
                    searchResult = FindQuotations(searchResult, searchValue);
                    break;
                case "Reservedele":
                    searchResult = FindSpareParts(searchResult, searchValue);
                    break;
                case "Kunder":
                    searchResult = FindCustomer(searchResult, searchValue);
                    break;
                case "Besøg":
                    searchResult = FindVisit(searchResult, searchValue);
                    break;
                default:
                    return searchResult;
            }

            return searchResult;
        }

        private SearchResultViewModel FindVisit(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Visits> results;

            if (searchValue == null)
            {
                results = _db.Visits;
            }
            else
            {
                results = from x in _db.Visits
                          where x.ContactPerson.Contains(searchValue) ||
                                x.Comment.Contains(searchValue) ||
                                x.Date.Contains(searchValue) ||
                                x.NextAppointment.Contains(searchValue)
                          select x;
            }

            model.VisitsViewModels = results.Select(x => new VisitsViewModel()
            {
                Id = x.Id,
                ContactPerson = x.ContactPerson,
                Comment = x.Comment,
                Date = x.Date,
                NextAppointment = x.NextAppointment,
                CustomerName = _db.Customers.FirstOrDefault(y => y.Visits.Contains(x)).Name,
                CustomerId = _db.Customers.FirstOrDefault(y => y.Visits.Contains(x)).Id
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindMachines(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Machine> results;

            if (searchValue == null)
            {
                results = _db.Machines;
            }
            else
            {
                results = from x in _db.Machines
                          where x.Name.Contains(searchValue)
                          select x;
            }

            model.MachineViewModels = results.Select(x => new MachineViewModel
            {
                Name = x.Name,
                DateCreated = x.DateCreated,
                IsSold = x.IsSold,
                SerialNumber = x.SerialNumber,
                Services = x.Services,
                SpareParts = x.SpareParts,
                Id = x.Id,
                CustomerId = _db.Customers.FirstOrDefault(y => y.Machines.Contains(x)).Id,
                CustomerName = _db.Customers.FirstOrDefault(y => y.Machines.Contains(x)).Name
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindContacts(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Contact> results;

            if (searchValue == null)
            {
                results = _db.Contacts;
            }
            else
            {
                results = from x in _db.Contacts
                          where x.Name.Contains(searchValue) ||
                                x.Email.Contains(searchValue) ||
                                x.PhoneNumber.Contains(searchValue)
                          select x;
            }

            model.ContactViewModels = results.Select(x => new ContactViewModel()
            {
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Id = x.Id,
                CustomerId = _db.Customers.FirstOrDefault(y => y.Contacts.Contains(x)).Id,
                CustomerName = _db.Customers.FirstOrDefault(y => y.Contacts.Contains(x)).Name
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindManuals(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Manual> results;

            if (searchValue == null)
            {
                results = _db.Manuals;
            }
            else
            {
                results = from x in _db.Manuals
                          where x.Name.Contains(searchValue) ||
                                x.FileName.Contains(searchValue)
                          select x;
            }

            model.ManualViewModels = results.Select(x => new ManualViewModel()
            {
                FileName = x.FileName,
                Name = x.Name,
                Id = x.Id
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindProspects(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Prospect> results;

            if (searchValue == null)
            {
                results = _db.Prospects;
            }
            else
            {
                results = from x in _db.Prospects
                          where x.Name.Contains(searchValue) ||
                                x.FileName.Contains(searchValue)
                          select x;
            }

            model.ProspectsViewModels = results.Select(x => new ProspectsViewModel()
            {
                FileName = x.FileName,
                Name = x.Name,
                Id = x.Id
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindQuotations(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Quotation> results;

            if (searchValue == null)
            {
                results = _db.Quotations;
            }
            else
            {
                results = from x in _db.Quotations
                          where x.Name.Contains(searchValue) ||
                                x.FileName.Contains(searchValue)
                          select x;
            }

            model.QuotationViewModels = results.Select(x => new QuotationViewModel()
            {
                FileName = x.FileName,
                Name = x.Name,
                Id = x.Id
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindSpareParts(SearchResultViewModel model, string searchValue)
        {
            IQueryable<SparePart> results;

            if (searchValue == null)
            {
                results = _db.SpareParts;
            }
            else
            {
                results = from x in _db.SpareParts
                          where x.Name.Contains(searchValue) ||
                                x.FileName.Contains(searchValue)
                          select x;
            }

            model.SparePartViewModels = results.Select(x => new SparePartViewModel()
            {
                Name = x.Name,
                FileName = x.FileName,
                Id = x.Id,
                MachineId = _db.Machines.FirstOrDefault(y => y.SpareParts.Contains(x)).Id,
                CustomerId = _db.Customers.FirstOrDefault(y => y.Machines.Contains(_db.Machines.FirstOrDefault(z => z.SpareParts.Contains(x)))).Id,
                CustomerName = _db.Customers.FirstOrDefault(y => y.Machines.Contains(_db.Machines.FirstOrDefault(z => z.SpareParts.Contains(x)))).Name,
                MachineName = _db.Machines.FirstOrDefault(y => y.SpareParts.Contains(x)).Name
            }).ToList();

            return model;
        }

        private SearchResultViewModel FindCustomer(SearchResultViewModel model, string searchValue)
        {
            IQueryable<Customer> results;

            if (searchValue == null)
            {
                results = _db.Customers;
            }
            else
            {
                results = from x in _db.Customers
                          where x.Address.Contains(searchValue) ||
                                x.Cvr.Contains(searchValue) ||
                                x.City.Contains(searchValue) ||
                                x.Email.Contains(searchValue) ||
                                x.Homepage.Contains(searchValue) ||
                                x.Name.Contains(searchValue) ||
                                x.PhoneNumber.Contains(searchValue) ||
                                x.PostalCode.Contains(searchValue)
                          select x;
            }

            model.Customers = results.Select(x => new CustomerViewModel()
            {
                Address = x.Address,
                Cvr = x.Cvr,
                City = x.City,
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                Homepage = x.Homepage,
                PostalCode = x.PostalCode
            }).ToList();

            return model;
        }
    }
}