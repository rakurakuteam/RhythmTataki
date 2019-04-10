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
                <button id="addCart" value="{{$product->id}}">장바구니</button>
                <input type="button" value="구매하기">
            </div>
        </div>
    </center>
</contents>

