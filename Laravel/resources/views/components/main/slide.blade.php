<!--이미지슬라이드-->
<center>
    <div id="slide">
        <input type="radio" name="pos" id="pos1" checked="checked"/>
        <input type="radio" name="pos" id="pos2"/>
        <input type="radio" name="pos" id="pos3"/>
        <input type="radio" name="pos" id="pos4"/>
        <ul>
            <li><img src="{{asset('images/pic/slide1.png')}}"/></li>
            <li><img src="{{asset('images/pic/slide2.png')}}"/></li>
            <li><img src="{{asset('images/pic/slide3.png')}}"/></li>
            <li><img src="{{asset('images/pic/slide4.png')}}"/></li>
        </ul>
        <p class="pos">
            <label for="pos1"></label>
            <label for="pos2"></label>
            <label for="pos3"></label>
            <label for="pos4"></label>
        </p>
    </div>
</center>