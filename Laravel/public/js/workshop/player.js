$(document).ajaxComplete(function(){
    console.log('1번')
    wavesurfer = WaveSurfer.create({
        height: 300,
        container: '#waveform',
        waveColor: "rgba(243, 243, 244, 1)", // 웨이브 색깔
        progressColor: "rgba(11,142,193, 1)", // 재생후 색깔
        pixelRatio: 1,
        scrollParent: false,
        normalize: true,
        plugins: [
            WaveSurfer.cursor.create({
                showTime: true,
                opacity: 0.7,
                customShowTimeStyle: {
                    'background-color': '#000',
                    color: '#fff',
                    padding: '2px',
                    'font-size': '14px'
                }
            }),
            WaveSurfer.regions.create() // 영역 생성
        ]
    });
    wavesurfer.load($('#temporary_sound').val());

    // 웨이브 서퍼가 준비되면
    wavesurfer.on('ready', function() {
        console.log("총 길이(초): "+timeInfo(wavesurfer.getDuration()))
        $('#play_time').text(timeInfo(wavesurfer.getDuration()));

        wavesurfer.enableDragSelection({
            color: "rgba(54,202,214, 0.5)",
        });
    });

    // 오디오가 재생되면 반복
    wavesurfer.on('audioprocess', function () {
        console.log(wavesurfer.getCurrentTime());
        $('#current_time').text(timeInfo(wavesurfer.getCurrentTime()));
    });

    // 영역이 변경되면
    wavesurfer.on('region-updated', function(region, e) {
        $('#start_sec').val(timeInfo(region.start));
        $('#end_sec').val(timeInfo(region.end));
        $('#len').val((region.end-region.start).toFixed(2));
        regionId = region.id;
    });

    // 영역 클릭시
    wavesurfer.on('region-click', function(region, e) {
        e.stopPropagation();
        // 반복 or 1번 재생
        e.shiftKey ? region.playLoop() : region.play();
        $('#start_sec').val(timeInfo(region.start));
        $('#end_sec').val(timeInfo(region.end));
        $('#len').val((region.end-region.start).toFixed(2));
        regionId = region.id;
    });

    // 영역 외부 클릭시
    wavesurfer.on('region-play', function(region) {
        region.once('out', function() {
            wavesurfer.play(region.start);
            wavesurfer.pause();
        });
    });

    // 시간 정보
    function timeInfo(time){
        var min = Math.floor(time/60);
        var sec = Math.floor(time-60*min);
        var ms = pad(Math.floor(((time-60*min).toFixed(2)-sec)*100), 2);
        if(ms == 100){
            ms = pad(0, 2);
            ++sec;
        }
        return min+":"+sec+"."+ms;
    };
    
    function pad(n, width) {
        n = n + '';
        return n.length >= width ? n : new Array(width - n.length + 1).join('0') + n;
    }

    // 오디오 재생
    document.getElementById('play_btn').addEventListener('click', function(){
        wavesurfer.play();
    });

    // 오디오 정지
    document.getElementById('pause_btn').addEventListener('click', function(){
        wavesurfer.pause();
    });
    // 영역 삭제
    document.getElementById('remove_btn').addEventListener('click',function(){
        wavesurfer.regions.list[regionId].remove();
    });

});
