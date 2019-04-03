<header>
    <center>
        <nav id="topMenu">
            <div id="logoBox">
                <a href="main.html">
                <img src="{{asset('images/pic/rythm4.png')}}" id="logo" />
                </a>
            </div>
            <ul>
                <li><a class="menuLink" id="li-1" href="#"><strong>{{__('messages.intro')}}</strong></a></li>
                <li><a class="menuLink" href="#"><strong>{{__('messages.soundGarden')}}</strong></a></li>
                <li><a class="menuLink" href="{{route('workshop.main')}}"><strong>{{__('messages.workshop')}}</strong></a></li>
                <li><a class="menuLink" href="#"><strong>{{__('messages.localization')}}</strong></a></li>
                <li><a class="menuLink" href="#"><strong>{{__('messages.store')}}</strong></a></li>
                <li></li>
                @if(\Auth::check())
                    <img src="{{asset('images/pic/user.png')}}" id="login-logo" />
                @else
                    <li><a class="menuLink" href="#"><strong>{{__('messages.login')}}</strong></a></li>
                @endif
            </ul>
        </nav>
    </center>
</header>