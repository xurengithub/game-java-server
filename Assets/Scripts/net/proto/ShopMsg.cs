
public class MsgBuy : MsgBase
{
    public int id;
    public int result = 0;
}

public class MsgBuyItem:MsgBase {
    public int result = 0;
    public int itemId;
    public MsgBuyItem(){
        protoName = "MsgBuyItem";
    }

}