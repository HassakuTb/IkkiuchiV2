namespace Ikkiuchi.Core {
    public interface ICard {

        int Id { get; }

        //  移動方向
        Direction MoveDirection { get; }

        //  上書きアクション
        IAction Action { get; }
    }

    public class Card : ICard{

        public int Id { get; private set; }

        public Direction MoveDirection { get; private set; }

        public IAction Action { get; private set; }

        
        public Card(int id, IAction action, Direction direction) {
            Id = id;
            Action = action;
            MoveDirection = direction;
        }
    }
}
