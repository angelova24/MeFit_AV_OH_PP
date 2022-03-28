<script setup>
    import { computed } from "vue";
    import { useStore } from "vuex";


    const store = useStore();
    const profile = computed(() => store.state.profile);
    const userStore = computed(() => store.state.user)
    const onAddProfile = () => {
        const newProfile = profile.value
        const user = userStore.value
        if (user.profileId === 0) {
            store.dispatch("addProfile", newProfile)
        }
        else{
            store.dispatch("updateProfile", newProfile)    
            console.log("profile updated")
        }        
    };
    
</script>

<template>
    <div>
        <header>
            <h1> Profile Page </h1>
            <h3>Before you start training you need a profile</h3>
        </header>
        <main>
           <span>Your current fitness evaluation:</span>
            <form title="fitnessEvaluation" >                
                <label for="weight">
                    Weight:
                </label>
                <input id="weight" type="text" v-model="profile.weight" />
                <label for="height">
                    Height:
                </label>
                <input id="height" type="text" v-model="profile.height" />
                <label for="medicalCondition">
                    Medical condition:
                </label>
                <input id="medicalCondition" type="text" v-model="profile.medicalConditions" />
                <label for="disabilities">
                    Disabilities:
                </label>
                <input id="disabilities" type="text" v-model="profile.disabilities" />
               
                <label for="fintessLevelEvaluation">
                    FintessLevelEvaluation:
                </label>
                <input id="fintessLevelEvaluation" type="text" v-model="profile.fintessLevelEvaluation" />
            </form>
            <form title="addressData" >
                <span>Your address data:</span>
                <label for="addressLine1">
                    Address line 1:
                </label>
                <input id="addressLine1" type="text" v-model="profile.addressLine1" />
                <label for="addressLine2">
                    Address line 2:
                </label>
                <input id="addressLine2" type="text" v-model="profile.addressLine2" />
                <label for="postalCode">
                    Postal code:
                </label>
                <input id="postalCode" type="text" v-model="profile.postalCode" />
                <label for="city">
                    City:
                </label>
                <input id="city" type="text" v-model="profile.city" />
                <label for="country">
                    Country:
                </label>
                <input id="country" type="text" v-model="profile.country" />
            </form>
            <button v-on:click="onAddProfile" type="submit">Save changes</button>
        </main>
    </div>
</template>

<style scoped>
input[type=text], select {
  width: 100%;
  padding: 10px 12px;
  margin: 6px 0px;
  display: inline-block;
  border: 1px solid #ccc;
  border-radius: 12px;
  box-sizing: border-box;
}

button {
  width: 100%;
  background-color: #4CAF50;
  color: white;
  padding: 14px 20px;
  margin: 8px 0;
  border: none;
  border-radius: 12px;
  cursor: pointer;
}

input:hover {
  background-color: #c0cac1;
}

div {
  border-radius: 5px;
  background-color: #f2f2f2;
  padding: 10px;
}
    form {
        display:flex;
        flex-wrap: wrap;
        row-gap: 5px;
        column-gap: 5px;
        align-items:stretch;
        margin-bottom: 10px;
    }
    header {
        margin-bottom: 20px;
    }
</style>
