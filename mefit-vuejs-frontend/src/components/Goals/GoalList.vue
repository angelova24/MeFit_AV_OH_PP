<script setup>
    import { useStore } from 'vuex';

    import GoalListItem from './GoalListItem.vue';

    const store = useStore();

    const props = defineProps({
        goals: {
            type: Array,
            required: true
        },
        header: {
            type: String,
            required: false
        }
    });

    const onGoalListItemClicked = (event, id) => {
        console.log(`GoalListItem with id ${id} was clicked...`)
        store.commit("setGoalDetailsId", id);
    }

</script>

<template>
    <div>
        <header>
            <b>{{ header }}</b>
        </header>
        <main>
            <GoalListItem 
                v-for="goal in goals" 
                v-bind:goal="goal"
                v-bind:key="goal.id"
                v-on:click="onGoalListItemClicked($event, goal.id)">
            </GoalListItem>
        </main>
    </div>
</template>

<style scoped>
    header {
        color: white;
        background-color: #333;
    }
</style>
