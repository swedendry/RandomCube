namespace UI.Game
{
    public class MyUserComponent : UserComponent
    {
        public override void Event(string param)
        {
            switch (param)
            {
                case "Create":
                    {
                        //user.CreateCube();
                    }
                    break;
            }
            base.Event(param);
        }
    }
}