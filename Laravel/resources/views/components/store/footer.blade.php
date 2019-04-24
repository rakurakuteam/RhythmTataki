<footer>
    @include('layouts.footer.footer')
    <!--하단고정메뉴바-->
    <div class="footer_menu">
        <img src="{{asset('images/pic/up_arrow.png')}}" id="up_arrow">
        <p id="up_text">{{__('messages.TOP')}}</p>
        <img src="{{asset('images/pic/minus-symbol.png')}}" class="minus_btn" id="minus_btn_0" onclick="quantity(event)">
        <input type="text" class="input" id="quantity_0" value="1">
        <img src="{{asset('images/pic/plus-button.png')}}" class="plus_btn" id="plus_btn_0" onclick="quantity(event)">
        <!--주문버튼-->
        <button class="shop_button order_button" onclick="location.href='{{route('orderListPage', ['id' => $product->id])}}'">
            <b>{{__('messages.orderRequest')}}</b>
        </button>
        <button class="shop_button add_cart_button" onclick="addCart({{$product->id}})">
            <b>{{__('messages.cart')}}</b>
        </button>
    </div>
</footer>