<template>
    <v-flex xs12="xs12">
        <v-card color="primary">
            <v-flex xs12>공방</v-flex>
            <v-flex xs12>드럼 소리 편집</v-flex>
            <v-flex xs12>
                <div id="waveform" @drag="drag"></div>
                <button @click="play">play/pause</button>
                <div div="sec">sec:</div>
            </v-flex>
        </v-card>
    </v-flex>
</template>

<script>
// import RegionsPlugin from '../wavesurfer/plugin/regions.js';
export default {
    mounted (){
        this.$nextTick(() => {
            this.wavesurfer = WaveSurfer.create({
                container: '#waveform',
                waveColor: 'blue', // 웨이브 색깔
                progressColor: 'black', // 재생후 색깔
                barHeight: 3, // 막대 높이
                barWidth: 1, // 막대 크기
                /* 커서 */
                container: document.querySelector('#waveform'), 
                plugins: [
                    WaveSurfer.cursor.create({
                        showTime: true,
                        opacity: 1,
                        customShowTimeStyle: {
                            'background-color': '#000',
                            color: '#fff',
                            padding: '2px',
                            'font-size': '10px'
                        }
                    }),
                    WaveSurfer.regions.create() // 영역 생성
                ]
            }),
            this.wavesurfer.load('http://ia902606.us.archive.org/35/items/shortpoetry_047_librivox/song_cjrg_teasdale_64kb.mp3');
            this.wavesurfer.addRegion({
                id: "1",
                start: 0,
                end: 40,
                drag: true,
                resize: true,
                color: "rgba(200, 100, 0, 0.1)"	
            })
            this.wavesurfer.getCurrentTime();
            // this.wavesurfer.addRegion();
        })
    },
    methods: {
        play(){
            this.wavesurfer.playPause()
        },
        drag(){
            this.wavesurfer.playPause()
        },
    }
}

</script>



