import './bootstrap';

if(window.location.pathname === '/workshop'){
    require('./workshop/wavesurfer/upload.js');
    require('./workshop/wavesurfer/player.js');
}