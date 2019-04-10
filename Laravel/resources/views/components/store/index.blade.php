<contents>
    <center>
        <div class="content_box">
            @foreach($products as $product)
                <div class="sub_photo" id="photo_gallery">
                    <p class="thmb">
                        <a href="{{route('store.show', $product->id)}}">
                        <img src="{{$product->images[0]->path.$product->images[0]->name}}" class="picture" href="#"></a>
                    </p>
                    <a href="{{route('store.show', $product->id)}}">
                        <div>
                            <h3>
                                {{$product->name}}
                            </h3>
                        </div>
                    </a>
                    <div>{{number_format($product->price)}}원</div>
                    <button type="button">주문하기</button>
                </div>
            @endforeach
        </div>
    </center>
</contents>