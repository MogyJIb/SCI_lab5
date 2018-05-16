namespace lab4.Models.Sorts
{
    public class TourKindSort
    {
        public Model Models { get; set; }

        public TourKindSort()
        {
            Models = new Model(State.NoSort);
        }

        public TourKindSort(State sortOrder)
        {
            Models = new Model(sortOrder);
        }

        public enum State
        {
            NoSort,

            NameAsc,
            NameDesc,

            DescriptionAsc,
            DescriptionDesc,

            ConstraintsAsc,
            ConstraintsDesc
        }

        public class Model
        {
            public State NameSort { get; set; } 
            public State DescriptionSort { get; set; }
            public State ConstraintsSort { get; set; }

            public State CurrentState { get; set; }    

            public Model(State sortOrder)
            {
                NameSort = sortOrder == State.NameAsc ? State.NameDesc : State.NameAsc;
                DescriptionSort = sortOrder == State.DescriptionAsc ? State.DescriptionDesc : State.DescriptionAsc;
                ConstraintsSort = sortOrder == State.ConstraintsAsc ? State.ConstraintsDesc : State.ConstraintsAsc;

                CurrentState = sortOrder;
            }
        }
    }
}
