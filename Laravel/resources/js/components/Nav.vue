<template>
    <v-layout row="row" wrap="wrap">
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0" @click="$router.push({name:'about'})" style="cursor:pointer">소개</v-card-text>
            </v-card>
        </v-flex>
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0" @click="$router.push({name:'home'})" style="cursor:pointer">소리마당</v-card-text>
            </v-card>
        </v-flex>
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0" @click="$router.push({name:'workshop'})" style="cursor:pointer">소리공방</v-card-text>
            </v-card>
        </v-flex>
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0">언어</v-card-text>
            </v-card>
        </v-flex>
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0">스토어</v-card-text>
            </v-card>
        </v-flex>
        <v-flex xs2="xs2">
            <v-card dark="dark" color="primary">
                <v-card-text class="px-0">로그인</v-card-text>
            </v-card>
        </v-flex>
    </v-layout>
</template>