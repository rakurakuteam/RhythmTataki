<contents>
    <div class="contents_box">
        <h2 id="basket_text">
            {{__('messages.cart')}}
        </h2>
        <div class="select_all">
            <input type="checkbox" id="cb">
            <label for="cb"></label><div class="select_all_text">전체선택</div>
        </div>
        @foreach($carts as $cart)
        <div class="basket_box">
            <div class="select_box">
                <input type="checkbox" class="cb" id="cb{{$count}}">
                <label for="cb{{$count}}"></label>
            </div>
            <img src="{{$cart->images[0]->path.$cart->images[0]->name}}" id="basket_img">
            <b><p id="shop_name_text">{{$cart->name}}</p></b>
            @include('components.store.quantityButton', ['count', $count])
            <img src="{{asset('images/pic/remove.png')}}" class="remove_button">
            <div class="price_box"><p><b id="price_text_{{$count++}}">{{$cart->price}}</b></p></div>
        </div>
        @endforeach
    </div>
</contents>
<footer>
    <!--하단고정메뉴바-->
    <div class="footer_menu">
        <div class="footer_total_text">
            <h2 class="price_4">합계&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;00000원</h2>
        </div>
        <button id="shop_button" href="#">
            <b>주문하기</b>
        </button>
    </div>
</footer>