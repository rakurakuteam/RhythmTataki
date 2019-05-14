@foreach ($files as $file)
    <li class="list-group-item"><p id="date">{{$file->created_at}}</p><p id="list_title">{{$file->name}}</p></li>
@endforeach