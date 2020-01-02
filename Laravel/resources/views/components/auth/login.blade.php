<form action="{{route('login')}}" method="POST">
    @csrf
    <input type="text" name="email" placeholder="email"><br>
    <input type="password" name="password" placeholder="password">
    <input type="submit" value="{{__('messages.login')}}">
</form>