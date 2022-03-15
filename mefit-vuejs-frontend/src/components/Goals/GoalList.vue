<script setup>
    import { useStore } from 'vuex';

    import GoalListItem from './GoalListItem.vue';

    const store = useStore();

    const props = defineProps({
        goals: {
            type: Array,
            required: true
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
            <b>List of available programs:</b>
        </header>
        <main>
            <ProgramListItem 
                v-for="program in programs" 
                v-bind:program="program"
                v-bind:key="program.id"
                v-on:click="onProgramListItemClicked($event, program.id)">
            </ProgramListItem>
        </main>
    </div>
</template>

<style scoped>

</style>
