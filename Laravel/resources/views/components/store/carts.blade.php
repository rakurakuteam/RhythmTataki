<contents>
    <h2 id="basket_text">
        {{__('messages.cart')}}
    </h2>
    <div class="select_all">
        <input type="checkbox" id="cb" class="cb">
        <label for="cb"></label><div class="select_all_text">{{__('messages.all_select')}}</div>
    </div>
    <div class="contents_box" id="contents_box">
        @include('components.store.basketBox')
    </div>
</contents>
<footer>
    <!--하단고정메뉴바-->
    <div class="footer_menu">
        <div class="footer_total_text">
          <h2 class="total">{{__('messages.sum')}}</h2>
              <h2 class="price">{{number_format($total_price)}}원</h2>
        </div>

        <button id="shop_button" onclick="location.href='{{route('orderSheetPage', ['id' => $product_id])}}'">
            <b>{{__('messages.orderRequest')}}</b>
        </button>
    </div>
</footer>
