<form name="uploadForm" id="uploadForm" enctype="multipart/form-data" method="post">
  <center id="workshop_center">
    <div class="contents_box">
      <div class="index_box">
        {{__('messages.workshop_index')}}
        <hr class="index_box_hr">
      </div>
      <div class="filebox" id="dropZone">
        <label for="ex_file">
            <img for="ex_file" src="{{asset('images/pic/mouse.png')}}" class="album_img">
        </label>
        <input type="file" name="file" id="ex_file" onchange="selectFile()">
        <h2 class="title_text">{{__('messages.file_select_message')}}</h2>
      </div>
    </div>
  </center>
</form>
