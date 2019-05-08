<!--리스트-->
<div class="list_box">
  <p id="list"><b>녹화영상</b></p>
    <div class="col-md-12 row">
        <center>
            <div class="col-md-6">
                @foreach ($files as $file)
                    <div class="radio_box">
                        <label class="radio list">{{$file->name}}<p id="date">{{$file->created_at}}</p>
                        <input type="radio" name="list" value="{{$file->name}}">
                        <span class="checkround"></span>
                        </label>
                    </div>
                @endforeach
            </div>
            <button class="btn cust-btn " type="button" id="btn-registration" style="font-size: 20px;letter-spacing: 1px;">Register</button>
        </center>
    </div>
</div>