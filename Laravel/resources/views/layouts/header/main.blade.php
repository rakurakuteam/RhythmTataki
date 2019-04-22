<header>
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
                <li><a class="menuLink" href="#"><strong>{{__('messages.localization')}}</strong></a></li>
                <li><a class="menuLink" href="{{route('store.index')}}"><strong>{{__('messages.store')}}</strong></a></li>
                <li></li>
                @if(\Auth::check())
                {{-- 로그아웃 --}}
                    <a href="{{route('logout')}}" onclick="event.preventDefault();document.getElementById('logout-form').submit();">
                        <img src="{{asset('images/pic/user.png')}}" id="login-logo" />
                    </a>
                    <form id="logout-form" action="{{route('logout')}}" method="POST" style="display: none;">
                        @csrf
                    </form>
                {{-- / 로그아웃 --}}
                @else
                    <li><a class="menuLink" href="{{route('login')}}"><strong>{{__('messages.login')}}</strong></a></li>
                @endif
            </ul>
        </nav>
    </center>
</header>
