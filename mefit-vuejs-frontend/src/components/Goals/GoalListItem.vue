<script setup>
    import { computed, toRefs, ref, onMounted } from 'vue';
    import { useStore } from "vuex";

    const props = defineProps({
        goal: {
            type: Object,
            required: true
        }
    });

    const { goal } = toRefs(props);
    const today = new Date();
    const currentDate = ref(today.toISOString().split('T')[0]);
    const goalStartDateString = ref(new Date(goal.value.startDate).toDateString());
    const goalEndDateString = ref(new Date(goal.value.endDate).toDateString());
    const numberOfDaysLeft = ref(NaN);
    onMounted(() => {
        console.log("onMounted GoalListItem...");
        if (goal !== undefined) {
            const newValue = (new Date(goal.value.endDate) - new Date(currentDate.value)) / (24 * 60 * 60 * 1000);
            console.log("Days left:", newValue);
            numberOfDaysLeft.value = Math.ceil(newValue);
        }
    });

    const store = useStore();
    const selectedGoalId = computed(() => store.state.goalDetailsId);

</script>

<template>
    <div v-bind:class="{ selected: goal.id === selectedGoalId }">
        <header>
            {{ goalStartDateString }} - {{ goalEndDateString }}
        </header>
        <main>
            <b>{{ numberOfDaysLeft }}</b> days left to reach this goal...
        </main>
    </div>
</template>

<style scoped>
    
    div {
        padding: 10px;
        background: rgb(250, 250, 192);
        border: 1px solid rgb(248, 248, 235);
    }

    div.selected {
        padding: 10px;
        background: rgb(250, 250, 135);
        border: 1px solid rgb(248, 243, 235);
    }

</style>
