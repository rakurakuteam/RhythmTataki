<div class="input_box">
    <img src="{{asset('images/pic/minus-symbol.png')}}" id="minus_btn_{{$count}}" class="minus_btn" onclick="quantity(event)">
    <input type="text" class="input quantity" id="quantity_{{$count}}" value="1">
    <img src="{{asset('images/pic/plus-button.png')}}" id="plus_btn_{{$count}}" class="plus_btn" onclick="quantity(event)">
</div>