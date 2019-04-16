<contents>
    <center>
        <div class="content_box">
            <div class="sub_photo" id="photo_gallery">
                <p class="thmb">
                    <img src="{{$path.$image}}" class="picture" href="#">
                </p>
                <div>
                    <h3>
                        {{$product->name}}
                    </h3>
                </div>
                <div>{{number_format($product->price)}}원</div>
                <div>{{$product->content}}</div>
                <button id="addCart" onclick="addCart({{$product->id}})">{{__('messages.cart')}}</button>
                <button id="buy" onclick="location.href='{{route('orderListPage', ['id' => $product->id])}}'">{{__('messages.buy')}}</button>
            </div>
        </div>
    </center>
</contents>

