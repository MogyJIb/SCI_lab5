namespace lab4.Models.Sorts
{
    public class TourSort
    {
        public Model Models { get; set; }

        public TourSort()
        {
            Models = new Model(State.NoSort);
        }

        public TourSort(State sortOrder)
        {
            Models = new Model(sortOrder);
        }

        public enum State
        {
            NoSort,

            NameAsc,
            NameDesc,

            PriceAsc,
            PriceDesc,

            StartDateAsc,
            StartDateDesc,

            EndDateAsc,
            EndDateDesc,

            ClientAsc,
            ClientDesc,

            TourKindAsc,
            TourKindDesc
    }

        public class Model
        {
            public State NameSort { get; set; } 
            public State PriceSort { get; set; }
            public State StartDateSort { get; set; }
            public State EndDateSort { get; set; }
            public State ClientSort { get; set; }
            public State TourKindSort { get; set; }

            public State CurrentState { get; set; }    

            public Model(State sortOrder)
            {
                NameSort = sortOrder == State.NameAsc ? State.NameDesc : State.NameAsc;
                PriceSort = sortOrder == State.PriceAsc ? State.PriceDesc : State.PriceAsc;
                StartDateSort = sortOrder == State.StartDateAsc ? State.StartDateDesc : State.StartDateAsc;
                EndDateSort = sortOrder == State.EndDateAsc ? State.EndDateDesc : State.EndDateAsc;
                ClientSort = sortOrder == State.ClientAsc ? State.ClientDesc : State.ClientAsc;
                TourKindSort = sortOrder == State.TourKindAsc ? State.TourKindDesc : State.TourKindAsc;

                CurrentState = sortOrder;
            }
        }
    }
}
