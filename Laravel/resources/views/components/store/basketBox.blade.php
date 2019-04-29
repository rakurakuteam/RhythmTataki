@foreach($carts as $cart)
    <div class="basket_box">
        <input type="text" id="product_id_{{$count}}" value="{{$cart->id}}" style="display:none">
        <div class="select_2">
        <div class="no_id_2"></div>
        <div class="no_id_1">&nbsp;</div>
            <input type="checkbox" class="cb" name="cb" id="cb{{$count}}">
            <label for="cb{{$count}}"></label>
        </div>
        <img src="{{$cart->images[0]->path.$cart->images[0]->name}}" id="basket_img">
        <b><p id="shop_name_text">{{$cart->name}}</p></b>
        @include('components.store.quantityButton', ['count', $count])
        <img src="{{asset('images/pic/remove.png')}}" class="remove_button" onclick="removeCart({{$cart->id}})">
        <div class="price_box"><p><b id="price_text_{{$count++}}">{{number_format($cart->price)}}</b></p></div>
    </div>
@endforeach