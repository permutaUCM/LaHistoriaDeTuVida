namespace LHDTV.Exceptions{
    public class WrongPasswordException: System.Exception{

        public int Id {get;}
        public WrongPasswordException(string msg,int id): base(msg){
            
                Id=id;
                
        }
    }
}