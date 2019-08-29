{{-- <header>
    <center>
        <nav id="topMenu">
            <div id="logoBox">
                <a href="{{route('home')}}">
<img src="{{asset('images/pic/rythm4.png')}}" id="logo" />
</a>
</div>
<ul>
    <li><a class="menuLink" id="li-1" href="#"><strong>{{__('messages.intro')}}</strong></a></li>
    <li><a class="menuLink" href="{{route('home')}}"><strong>{{__('messages.soundGarden')}}</strong></a></li>
    <li><a class="menuLink" href="{{route('workshop.main')}}"><strong>{{__('messages.workshop')}}</strong></a></li>
    @if(Session::get('locale') == 'ko')
    <li><a class="menuLink"
            href="{{route('locale.change', ['locale' => 'ja'])}}"><strong>{{__('messages.localization')}}</strong></a>
    </li>
    @else
    <li><a class="menuLink"
            href="{{route('locale.change', ['locale' => 'ko'])}}"><strong>{{__('messages.localization')}}</strong></a>
    </li>
    @endif
    <li><a class="menuLink" href="{{route('store.index')}}"><strong>{{__('messages.store')}}</strong></a></li>
    <li></li>
    @if(\Auth::check()) --}}
    {{-- 로그아웃 --}}
    {{-- <a href="{{route('logout')}}"
    onclick="event.preventDefault();document.getElementById('logout-form').submit();">
    <img src="{{asset('images/pic/user.png')}}" id="login-logo" />
    </a>
    <form id="logout-form" action="{{route('logout')}}" method="POST" style="display: none;">
        @csrf
    </form> --}}
    {{-- / 로그아웃 --}}
    {{-- @else
                    <li><a class="menuLink" href="{{route('login')}}"><strong>{{__('messages.login')}}</strong></a>
    </li>
    @endif
</ul>
</nav>
</center>
</header> --}}

<center>
    @if(!empty(Session::get('login_back')) && Session::get('login_back') == true)
        <script>
        $(function() {
            $('#loginModal').modal('show');
        });
        </script>
    @endif
    <img src="{{asset('images/pic/upper_bar.png')}}" id="upper_bar"/>
    <div id="login_menu">
        @if(!Auth::check())
        <!-- 로그인 안 했으면 -->
        <a href="#loginModal" class="text-uppercase" data-toggle="modal" data-backdrop="static"
            data-keyboard="false">{{__('messages.login')}}</a> / <a href="#joinModal" class="text-uppercase"
            data-toggle="modal" data-backdrop="static" data-keyboard="false">{{__('messages.join')}}</a>
        @endif
        @if(Auth::check())
        <!-- 로그인 했으면 -->
        <strong class="text-uppercase">{{Auth::user()['name']}} <i class="fa fa-caret-down"></i></strong>
        {{-- logout --}}
        <li>
            <a href="{{route('logout')}}" class="text-uppercase" onclick="event.preventDefault();document.getElementById('logout-form').submit();">
                <i class="fas fa-sign-out-alt"></i> {{__('messages.logout')}}
            </a>
        </li>
        <form id="logout-form" action="{{route('logout')}}" method="POST" style="display: none;">
            @csrf
        </form>
        {{-- /logout--}}
        @endif
    </div>
    <nav id="topMenu">
        <div id="logoBox">
            <a href="{{route('home')}}">
                <img src="{{asset('images/pic/rythm4.png')}}" id="logo" />
            </a>
        </div>
        <ul>
            <li><a class="menuLink" id="li-1" href="#"><strong>{{__('messages.intro')}}</strong></a></li>
            <li><a class="menuLink" href="{{route('home')}}"><strong>{{__('messages.soundGarden')}}</strong></a></li>
            <li><a class="menuLink" href="{{route('workshop.main')}}"><strong>{{__('messages.workshop')}}</strong></a>
            </li>
            @if(Session::get('locale') == 'ko')
            <li><a class="menuLink"
                    href="{{route('locale.change', ['locale' => 'ja'])}}"><strong>{{__('messages.localization')}}</strong></a>
            </li>
            @else
            <li><a class="menuLink"
                    href="{{route('locale.change', ['locale' => 'ko'])}}"><strong>{{__('messages.localization')}}</strong></a>
            </li>
            @endif
            <li><a class="menuLink" href="{{route('store.index')}}"><strong>{{__('messages.store')}}</strong></a></li>
            <li></li>

</center>
