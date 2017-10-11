using System.Collections.Generic;
using System.Linq;

namespace BallingMaskiner.Models
{
    public class SearchResultViewModel
    {
        public SearchResultViewModel()
        {
            MachineViewModels = new List<MachineViewModel>();
            ContactViewModels = new List<ContactViewModel>();
            ManualViewModels = new List<ManualViewModel>();
            ProspectsViewModels = new List<ProspectsViewModel>();
            QuotationViewModels = new List<QuotationViewModel>();
            SparePartViewModels = new List<SparePartViewModel>();
            VisitsViewModels = new List<VisitsViewModel>();
            Customers = new List<CustomerViewModel>();
        }

        public bool AnyResult => CheckLists();
        public List<MachineViewModel> MachineViewModels { get; set; }
        public List<ContactViewModel> ContactViewModels { get; set; }
        public List<ManualViewModel> ManualViewModels { get; set; }
        public List<ProspectsViewModel> ProspectsViewModels { get; set; }
        public List<QuotationViewModel> QuotationViewModels { get; set; }
        public List<SparePartViewModel> SparePartViewModels { get; set; }
        public List<VisitsViewModel> VisitsViewModels { get; set; }
        public List<CustomerViewModel> Customers { get; set; }
        

        private bool CheckLists()
        {
            return MachineViewModels.Any() || ContactViewModels.Any() || ManualViewModels.Any() ||
                   ProspectsViewModels.Any() || QuotationViewModels.Any() || SparePartViewModels.Any() ||
                   Customers.Any() || VisitsViewModels.Any();
        }
    }
}