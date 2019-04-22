<footer>
    @include('layouts.footer.footer')
    <!--하단고정메뉴바-->
    <div class="footer_menu">
        <img src="{{asset('images/pic/up_arrow.png')}}" id="up_arrow">
        <p id="up_text">{{__('messages.TOP')}}</p>
        <img src="{{asset('images/pic/minus-symbol.png')}}" id="minus_btn" onclick="plus_minus(1)">
        <input type="text" class="input" id="quantity" value="1">
        <img src="{{asset('images/pic/plus-button.png')}}" id="plus_btn" onclick="plus_minus(0)">
        <button id="shop_button2">
            <b>{{__('messages.orderRequest')}}</b>
        </button>
        <button id="shop_button1" href="#">
            <b>{{__('messages.cart')}}</b>
        </button>
    </div>
</footer>