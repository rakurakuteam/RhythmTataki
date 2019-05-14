<contents>
    <div class="contents">
        <h2 id="title_text">{{__('messages.orderHistory')}}</h2>
        @foreach($orders as $order)    
            <!--박스-->
            <div class="tracking_box">
                <img src="{{asset('images/pic/drumSet.png')}}" id="shop_img">
                <div class="text_box_1">

                    <b><p id="shop_name_text">{{$order->product->name}}</p></b>
                    <p id="shop_num">주문번호 {{$order->order_num}}</p>
                    <p id="shop_date">{{$order->created_at}}</p>

                </div>

                <div class="box">
                        <div class="quantity_box">
                            <p id="quantity_text"><b>{{__('messages.quantity')}}</b></p>
                            <p id="quantity">{{$order->quantity}}개</p>
                        </div>
                        <div class="track_box">
                            <p id="track_text"><b>{{__('messages.status')}}</b></p>
                            <p id="track">{{$order->delivery_status->status}}</p>
                        </div>
                        <div class="price_box">
                            <p id="price_text"><b>{{__('messages.price')}}</b></p>
                            <p id="price">{{number_format($order->product->price*$order->quantity)}}원</p>
                        </div>
                </div>
            </div>
        @endforeach
    </div>
</contents>
