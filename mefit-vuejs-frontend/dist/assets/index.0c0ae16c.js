var K=Object.defineProperty,Y=Object.defineProperties;var Q=Object.getOwnPropertyDescriptors;var O=Object.getOwnPropertySymbols;var X=Object.prototype.hasOwnProperty,Z=Object.prototype.propertyIsEnumerable;var j=(e,o,t)=>o in e?K(e,o,{enumerable:!0,configurable:!0,writable:!0,value:t}):e[o]=t,N=(e,o)=>{for(var t in o||(o={}))X.call(o,t)&&j(e,t,o[t]);if(O)for(var t of O(o))Z.call(o,t)&&j(e,t,o[t]);return e},V=(e,o)=>Y(e,Q(o));import{u as k,c as _,a as ee,r as C,b as A,o as a,d as c,e as s,f as v,w as L,g as i,h as x,v as te,t as f,i as d,p as b,j as I,k as E,F as W,l as T,m as U,n as h,q as w,s as oe,x as se,y as re,z as ne,K as ie,A as le}from"./vendor.9fdab923.js";const ae=function(){const o=document.createElement("link").relList;if(o&&o.supports&&o.supports("modulepreload"))return;for(const r of document.querySelectorAll('link[rel="modulepreload"]'))l(r);new MutationObserver(r=>{for(const n of r)if(n.type==="childList")for(const u of n.addedNodes)u.tagName==="LINK"&&u.rel==="modulepreload"&&l(u)}).observe(document,{childList:!0,subtree:!0});function t(r){const n={};return r.integrity&&(n.integrity=r.integrity),r.referrerpolicy&&(n.referrerPolicy=r.referrerpolicy),r.crossorigin==="use-credentials"?n.credentials="include":r.crossorigin==="anonymous"?n.credentials="omit":n.credentials="same-origin",n}function l(r){if(r.ep)return;r.ep=!0;const n=t(r);fetch(r.href,n)}};ae();var $=(e,o)=>{const t=e.__vccOpts||e;for(const[l,r]of o)t[l]=r;return t};const F=e=>(b("data-v-16e2c308"),e=e(),I(),e),ce=d(" Navigation Bar: "),ue=d(" Dashboard "),de=d(" Goals "),_e=d(" Programs "),pe=d(" Workouts "),he=d(" Exercises "),ge={value:"username"},fe=F(()=>s("option",{value:"showprofile"},"show my profile...",-1)),me=F(()=>s("option",{value:"logout"},"log me out...",-1)),ve={emits:["logout"],setup(e,{emit:o}){const t=k(),l=_(()=>t.state.user.username),r=_(()=>t.state.baseUrl),n=ee(),u=C("username"),p=g=>{switch(console.log(`new SelectUser value: ${g.target.value}`),g.target.value){case"showprofile":n.push(`${r}profile`),u.value="username";break;case"logout":o("logout");break}};return(g,m)=>{const D=A("router-link");return a(),c("div",null,[s("nav",null,[ce,v(D,{to:i(r)+"dashboard","active-class":"active"},{default:L(()=>[ue]),_:1},8,["to"]),v(D,{to:i(r)+"goals","active-class":"active"},{default:L(()=>[de]),_:1},8,["to"]),v(D,{to:i(r)+"programs","active-class":"active"},{default:L(()=>[_e]),_:1},8,["to"]),v(D,{to:i(r)+"workouts","active-class":"active"},{default:L(()=>[pe]),_:1},8,["to"]),v(D,{to:i(r)+"exercises","active-class":"active"},{default:L(()=>[he]),_:1},8,["to"]),x(s("select",{onChange:p,"onUpdate:modelValue":m[0]||(m[0]=J=>u.value=J)},[s("option",ge,"logged in as: "+f(i(l)),1),fe,me],544),[[te,u.value]])])])}}};var ke=$(ve,[["__scopeId","data-v-16e2c308"]]);const ye={};function $e(e,o){return a(),c("div",null," \xA9 2022 by Vilyana Angelova, Petar Petrov, Oliver Hauck ")}var xe=$(ye,[["render",$e]]);const we=s("hr",null,null,-1),be=s("hr",null,null,-1),Ie={props:["keycloak"],setup(e){const o=e,{keycloak:t}=E(o),l=k(),r=()=>{console.log("reading data from Db..."),l.dispatch("fetchExcercises"),l.dispatch("fetchSets"),l.dispatch("fetchWorkouts"),l.dispatch("fetchPrograms"),l.dispatch("fetchUser").then(g=>{console.log("User received in promise:",g),l.dispatch("fetchProfile",g.profileId).then(m=>l.dispatch("fetchGoals",m.goals))})},n=g=>{t.value.updateToken(g).then(m=>{console.log(m?"Token was successfully refreshed...":"Token is still valid..."),l.commit("setToken",t.value.token),console.log("Token:",t.value.token)}).catch(m=>{console.log("Failed to refresh the token, or the session has expired: ",m)})};t.value.onReady=g=>{g?(console.log("User was successfully authenticated..."),l.commit("setToken",t.value.token),r()):console.log("User authentication failed...")},t.value.onTokenExpired=g=>{console.log("Token expired:",g),n(5)};const u=()=>{n(5e4)},p=g=>{const m={redirectUri:window.location.protocol+"//"+window.location.host+"/login"};console.log("current Url:",m.redirectUri),t.value.logout(m).then(D=>{console.log("You have been logged out...",D)})};return(g,m)=>{const D=A("router-view");return a(),c("div",null,[s("button",{onClick:u},"Generate Token"),s("button",{onClick:r},"Read data from Db"),v(ke,{onLogout:p}),we,v(D),be,v(xe)])}}},De={props:{workout:{type:Object,required:!0}},setup(e){return(o,t)=>(a(),c("div",null,[s("header",null,f(e.workout.name),1)]))}},Pe=s("header",null,[s("b",null,"List of available workouts:")],-1),B={props:{workouts:{type:Array,required:!0}},setup(e){const o=k(),t=(l,r)=>{console.log(`WorkoutListItem with id ${r} was clicked...`),o.commit("setWorkoutDetailsId",r)};return(l,r)=>(a(),c("div",null,[Pe,s("main",null,[(a(!0),c(W,null,T(e.workouts,n=>(a(),U(De,{workout:n,key:n.id,onClick:u=>t(u,n.id)},null,8,["workout","onClick"]))),128))])]))}};const Se=e=>(b("data-v-6cc88c36"),e=e(),I(),e),Ue={key:0},Le={title:"achieved"},Ce=Se(()=>s("b",null,"completed:",-1)),Ee={title:"workouts"},We=d(" This goal comprises the following wokouts: "),Te={props:{goal:{type:Object,required:!1}},setup(e){const o=e,{goal:t}=E(o),l=C(t.value.startDate.toDateString()),r=C(t.value.endDate.toDateString()),n=k(),u=_(()=>{const p=[];if(t.value.workouts!==void 0){console.log("Workouts",t.value.workouts);for(const g of t.value.workouts){console.log("workoutId:",g);const m=_(()=>n.getters.getWorkoutById(g));console.log("tempWorkout:",m.value),p.push(m.value)}}return console.log(p),p});return(p,g)=>i(t)!==void 0?(a(),c("div",Ue,[s("header",null,[s("b",null,f(l.value)+" - "+f(r.value),1)]),s("main",null,[s("section",Le,[Ce,d(" "+f(i(t).achieved),1)]),s("section",Ee,[We,i(u)[0]!==void 0?(a(),U(B,{key:0,workouts:i(u)},null,8,["workouts"])):h("",!0)])])])):h("",!0)}};var Be=$(Te,[["__scopeId","data-v-6cc88c36"]]);const Ge=e=>(b("data-v-ee39fd82"),e=e(),I(),e),Oe=Ge(()=>s("header",null," Dashboard ",-1)),je={title:"calender"},Ne=["value"],Ve={key:0,title:"currentGoal"},Ae=d(" days left to reach this goal... "),Fe={setup(e){const o=new Date,t=C(o.toISOString().split("T")[0]),l=k(),r=_(()=>l.getters.getGoalById(1)),n=_(()=>{let u=NaN;return r.value!==void 0&&(u=Math.ceil((r.value.endDate-new Date(t.value))/(24*60*60*1e3))),u});return(u,p)=>(a(),c(W,null,[Oe,s("main",null,[s("section",je,[s("input",{type:"date",value:t.value},null,8,Ne)]),i(r)!==void 0?(a(),c("section",Ve,[v(Be,{goal:i(r)},null,8,["goal"]),s("b",null,f(i(n)),1),Ae])):h("",!0)])],64))}};var Re=$(Fe,[["__scopeId","data-v-ee39fd82"]]);const qe=s("header",null," Login Page ",-1),ze=s("label",null,"UserName: ",-1),Me={setup(e){let o=C("");const t=l=>{console.log(`Button ${l.target.value} was clicked...`),console.log(`entered username: ${o.value}`)};return(l,r)=>(a(),c("div",null,[qe,s("main",null,[ze,x(s("input",{type:"text","onUpdate:modelValue":r[0]||(r[0]=n=>oe(o)?o.value=n:o=n),placeholder:"username goes here..."},null,512),[[w,i(o),void 0,{trim:!0}]]),s("button",{type:"submit",onClick:t},"Login")])]))}},He={};function Je(e,o){return a(),c("div",null," Goals Page ")}var Ke=$(He,[["render",Je]]);const Ye={props:{exercise:{type:Object,required:!0}},setup(e){return(o,t)=>(a(),c("div",null,[s("header",null,f(e.exercise.name),1)]))}},Qe=s("header",null,[s("b",null,"List of available exercises:")],-1),Xe={props:{exercises:{type:Array,required:!0}},setup(e){const o=k(),t=(l,r)=>{console.log(`ExerciseListItem with id ${r} was clicked...`),o.commit("setExerciseDetailsId",r)};return(l,r)=>(a(),c("div",null,[Qe,s("main",null,[(a(!0),c(W,null,T(e.exercises,n=>(a(),U(Ye,{exercise:n,key:n.id,onClick:u=>t(u,n.id)},null,8,["exercise","onClick"]))),128))])]))}};const Ze=e=>(b("data-v-c5cfa562"),e=e(),I(),e),et={title:"description"},tt={title:"targetMuscleGroup"},ot=Ze(()=>s("b",null,"Target muscle group:",-1)),st={title:"image"},rt=["src"],nt={key:1},it={title:"video"},lt=["href"],at=["src"],ct={props:{exercise:{type:Object,required:!0}},setup(e){return(o,t)=>(a(),c("div",null,[s("header",null,[s("b",null,f(e.exercise.name),1)]),s("main",null,[s("section",et,f(e.exercise.description),1),s("section",tt,[ot,d(" "+f(e.exercise.targetMuscleGroup),1)]),s("section",st,[e.exercise.imageURL!==null?(a(),c("img",{key:0,src:e.exercise.imageURL},null,8,rt)):h("",!0),e.exercise.imageURL===null?(a(),c("div",nt,"no image available...")):h("",!0)]),s("section",it,[e.exercise.videoURL.toLowerCase().endsWith(".gif")?h("",!0):(a(),c("a",{key:0,href:e.exercise.videoURL}," Example-Video ",8,lt)),e.exercise.videoURL.toLowerCase().endsWith(".gif")?(a(),c("img",{key:1,src:e.exercise.videoURL},null,8,at)):h("",!0)])])]))}};var ut=$(ct,[["__scopeId","data-v-c5cfa562"]]);const R=e=>(b("data-v-06d0c0de"),e=e(),I(),e),dt=R(()=>s("header",null," Exercise Page ",-1)),_t={key:0,title:"exerciseList"},pt={key:1,title:"exerciseDetails"},ht=d(" Details of exercise: "),gt={key:2},ft=d(" We are sorry, but currently no exercises are available."),mt=R(()=>s("br",null,null,-1)),vt=d(" Please try again later... "),kt=[ft,mt,vt],yt={setup(e){const o=k(),t=_(()=>o.state.exercises),l=_(()=>o.state.exerciseDetailsId);console.log(`ExercisePage: selected exerciseid: ${l.value}`);const r=_(()=>o.getters.getExerciseById(l.value));return console.log(`ExercisePage: selected exercise: ${r.value}`),(n,u)=>(a(),c("div",null,[dt,s("main",null,[JSON.stringify(i(t))!=="[]"?(a(),c("section",_t,[v(Xe,{exercises:i(t)},null,8,["exercises"])])):h("",!0),i(r)!==void 0?(a(),c("section",pt,[ht,v(ut,{exercise:i(r)},null,8,["exercise"])])):h("",!0),JSON.stringify(i(t))==="[]"?(a(),c("section",gt,kt)):h("",!0)])]))}};var $t=$(yt,[["__scopeId","data-v-06d0c0de"]]);const xt=s("header",null,[s("b",null,"List of sets:")],-1),wt=d(" repetitions of "),bt={props:{sets:{type:Array,required:!0}},setup(e){const o=e,t=k(),{sets:l}=E(o),r=_(()=>{const n=[];for(const u of l.value){const p=_(()=>t.getters.getExerciseById(u.exerciseId));n.push(V(N({},u),{exerciseName:p.value.name}))}return n});return(n,u)=>(a(),c("div",null,[xt,s("main",null,[s("ul",null,[(a(!0),c(W,null,T(i(r),p=>(a(),c("li",{key:p.id},[s("b",null,f(p.exerciseRepetitions),1),wt,s("b",null,f(p.exerciseName),1)]))),128))])])]))}};const It=e=>(b("data-v-d478c986"),e=e(),I(),e),Dt={key:0},Pt={title:"type"},St=It(()=>s("b",null,"Type:",-1)),Ut={title:"exerciseSets"},Lt=d(" This workout comprises the following sets of exercises: "),Ct={props:{workout:{type:Object,required:!0}},setup(e){const o=e,{workout:t}=E(o),l=k(),r=_(()=>{console.log("WorkoutSets",t.value.sets);const n=[];for(const u of t.value.sets){console.log("setId:",u);const p=_(()=>l.getters.getSetById(u));console.log("tempSet:",p.value),n.push(p.value)}return console.log(n),n});return(n,u)=>i(t)!==void 0?(a(),c("div",Dt,[s("header",null,[s("b",null,f(i(t).name),1)]),s("main",null,[s("section",Pt,[St,d(" "+f(i(t).type),1)]),s("section",Ut,[Lt,i(r)[0]!==void 0?(a(),U(bt,{key:0,sets:i(r)},null,8,["sets"])):h("",!0)])])])):h("",!0)}};var Et=$(Ct,[["__scopeId","data-v-d478c986"]]);const q=e=>(b("data-v-19d210e2"),e=e(),I(),e),Wt=q(()=>s("header",null," Workout Page ",-1)),Tt={key:0,title:"workoutList"},Bt={key:1,title:"workoutDetails"},Gt=d(" Details of workout: "),Ot={key:2},jt=d(" We are sorry, but currently no workouts are available."),Nt=q(()=>s("br",null,null,-1)),Vt=d(" Please try again later... "),At=[jt,Nt,Vt],Ft={setup(e){const o=k(),t=_(()=>o.state.workouts),l=_(()=>o.state.workoutDetailsId);console.log(`selected workout id: ${l.value}`);const r=_(()=>o.getters.getWorkoutById(l.value));return console.log(`selected workout: ${r.value}`),(n,u)=>(a(),c("div",null,[Wt,s("main",null,[JSON.stringify(i(t))!=="[]"?(a(),c("section",Tt,[v(B,{workouts:i(t)},null,8,["workouts"])])):h("",!0),i(r)!=null?(a(),c("section",Bt,[Gt,v(Et,{workout:i(r)},null,8,["workout"])])):h("",!0),JSON.stringify(i(t))==="[]"?(a(),c("section",Ot,At)):h("",!0)])]))}};var Rt=$(Ft,[["__scopeId","data-v-19d210e2"]]);const qt={props:{program:{type:Object,required:!0}},setup(e){return(o,t)=>(a(),c("div",null,[s("header",null,f(e.program.name),1)]))}},zt=s("header",null,[s("b",null,"List of available programs:")],-1),Mt={props:{programs:{type:Array,required:!0}},setup(e){const o=k(),t=(l,r)=>{console.log(`ProgramListItem with id ${r} was clicked...`),o.commit("setProgramDetailsId",r)};return(l,r)=>(a(),c("div",null,[zt,s("main",null,[(a(!0),c(W,null,T(e.programs,n=>(a(),U(qt,{program:n,key:n.id,onClick:u=>t(u,n.id)},null,8,["program","onClick"]))),128))])]))}};const Ht=e=>(b("data-v-4efe646d"),e=e(),I(),e),Jt={key:0},Kt={title:"category"},Yt=Ht(()=>s("b",null,"Category:",-1)),Qt={title:"workouts"},Xt=d(" This program comprises the following wokouts: "),Zt={props:{program:{type:Object,required:!1}},setup(e){const o=e,{program:t}=E(o),l=k(),r=_(()=>{console.log("Workouts",t.value.workouts);const n=[];for(const u of t.value.workouts){console.log("workoutId:",u);const p=_(()=>l.getters.getWorkoutById(u));console.log("tempWorkout:",p.value),n.push(p.value)}return console.log(n),n});return(n,u)=>i(t)!==void 0?(a(),c("div",Jt,[s("header",null,[s("b",null,f(i(t).name),1)]),s("main",null,[s("section",Kt,[Yt,d(" "+f(i(t).category),1)]),s("section",Qt,[Xt,i(r)[0]!==void 0?(a(),U(B,{key:0,workouts:i(r)},null,8,["workouts"])):h("",!0)])])])):h("",!0)}};var eo=$(Zt,[["__scopeId","data-v-4efe646d"]]);const z=e=>(b("data-v-974cb4c4"),e=e(),I(),e),to=z(()=>s("header",null," Program Page ",-1)),oo={key:0,title:"programList"},so={key:1,title:"programDetails"},ro=d(" Details of program: "),no={key:2},io=d(" We are sorry, but currently no programs are available."),lo=z(()=>s("br",null,null,-1)),ao=d(" Please try again later... "),co=[io,lo,ao],uo={setup(e){const o=k(),t=_(()=>o.state.programs);console.log(JSON.stringify(t.value));const l=_(()=>o.state.programDetailsId);console.log(`selected program id: ${l.value}`);const r=_(()=>o.getters.getProgramById(l.value));return console.log(`selected program: ${r.value}`),(n,u)=>(a(),c("div",null,[to,s("main",null,[JSON.stringify(i(t))!=="[]"?(a(),c("section",oo,[v(Mt,{programs:i(t)},null,8,["programs"])])):h("",!0),i(r)!==void 0?(a(),c("section",so,[ro,v(eo,{program:i(r)},null,8,["program"])])):h("",!0),JSON.stringify(i(t))==="[]"?(a(),c("section",no,co)):h("",!0)])]))}};var _o=$(uo,[["__scopeId","data-v-974cb4c4"]]);const y=e=>(b("data-v-3ba3b1f0"),e=e(),I(),e),po=y(()=>s("header",null," Profile Page ",-1)),ho={title:"fitnessEvaluation"},go=y(()=>s("span",null,"Your current fitness evaluation:",-1)),fo=y(()=>s("label",{for:"weight"}," Weight: ",-1)),mo=y(()=>s("label",{for:"height"}," Height: ",-1)),vo=y(()=>s("label",{for:"medicalCondition"}," Medical condition: ",-1)),ko=y(()=>s("label",{for:"disabilities"}," Disabilities: ",-1)),yo={title:"addressData"},$o=y(()=>s("span",null,"Your address data:",-1)),xo=y(()=>s("label",{for:"addressLine1"}," Address line 1: ",-1)),wo=y(()=>s("label",{for:"addressLine2"}," Address line 2: ",-1)),bo=y(()=>s("label",{for:"postalCode"}," Postal code: ",-1)),Io=y(()=>s("label",{for:"city"}," City: ",-1)),Do=y(()=>s("label",{for:"country"}," Country: ",-1)),Po=y(()=>s("button",{type:"submit"},"Save changes",-1)),So={setup(e){const o=k(),t=_(()=>o.state.profile);return(l,r)=>(a(),c("div",null,[po,s("main",null,[s("section",ho,[go,fo,x(s("input",{id:"weight",type:"text","onUpdate:modelValue":r[0]||(r[0]=n=>i(t).weight=n)},null,512),[[w,i(t).weight]]),mo,x(s("input",{id:"height",type:"text","onUpdate:modelValue":r[1]||(r[1]=n=>i(t).height=n)},null,512),[[w,i(t).height]]),vo,x(s("input",{id:"medicalCondition",type:"text","onUpdate:modelValue":r[2]||(r[2]=n=>i(t).medicalCondition=n)},null,512),[[w,i(t).medicalCondition]]),ko,x(s("input",{id:"disabilities",type:"text","onUpdate:modelValue":r[3]||(r[3]=n=>i(t).disabilities=n)},null,512),[[w,i(t).disabilities]])]),s("section",yo,[$o,xo,x(s("input",{id:"addressLine1",type:"text","onUpdate:modelValue":r[4]||(r[4]=n=>i(t).addressLine1=n)},null,512),[[w,i(t).addressLine1]]),wo,x(s("input",{id:"addressLine2",type:"text","onUpdate:modelValue":r[5]||(r[5]=n=>i(t).addressLine2=n)},null,512),[[w,i(t).addressLine2]]),bo,x(s("input",{id:"postalCode",type:"text","onUpdate:modelValue":r[6]||(r[6]=n=>i(t).postalCode=n)},null,512),[[w,i(t).postalCode]]),Io,x(s("input",{id:"city",type:"text","onUpdate:modelValue":r[7]||(r[7]=n=>i(t).city=n)},null,512),[[w,i(t).city]]),Do,x(s("input",{id:"country",type:"text","onUpdate:modelValue":r[8]||(r[8]=n=>i(t).country=n)},null,512),[[w,i(t).country]])]),Po])]))}};var Uo=$(So,[["__scopeId","data-v-3ba3b1f0"]]);const S="https://mefitapi-va-pp-oh.azurewebsites.net/api",G=se({state:{baseUrl:"",userIdentity:{id:"",username:"",firstName:"",lastName:"",email:"",emailVerified:""},token:"",user:{id:0,username:"",name:"",isContributor:!1,isAdmin:!1,profileId:0},profile:{id:0,weight:0,height:0,medicalCondition:"",disabilities:"",addressLine1:"",addressLine2:"",postalCode:"",city:"",country:"",goalIds:[]},exercises:[],exerciseDetailsId:0,sets:[],workouts:[],workoutDetailsId:0,programs:[],programDetailsId:0,goals:[],goalDetailsId:0},mutations:{setBaseUrl:(e,o)=>{e.baseUrl=o},setUserIdentity:(e,o)=>{e.userIdentity=o},setToken:(e,o)=>{console.log("token in store will be set to:",o),e.token=o},setUser:(e,o)=>{console.log("user will be set to:",o),e.user=o},setProfile:(e,o)=>{console.log("profile will be set to:",o),e.profile=o},addExercises:(e,o)=>{for(const t of o)e.exercises.push(t)},setExerciseDetailsId:(e,o)=>{e.exerciseDetailsId=o},addSets:(e,o)=>{for(const t of o)e.sets.push(t)},addWorkouts:(e,o)=>{for(const t of o)e.workouts.push(t)},setWorkoutDetailsId:(e,o)=>{e.workoutDetailsId=o},addPrograms:(e,o)=>{for(const t of o)e.programs.push(t)},setProgramDetailsId:(e,o)=>{e.programDetailsId=o},addGoals:(e,o)=>{for(const t of o)e.goals.push(t)},setGoalDetailsId:(e,o)=>{e.goalDetailsId=o}},actions:{fetchExcercises:async e=>{console.log("fetching exercises from Db...");const o=await fetch(`${S}/Exercises`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!o.ok)console.log("FetchExercises from Db failed...!!!");else{const t=await o.json();e.commit("addExercises",t),console.log("FetchExercises from Db done..."),console.log("Exercises received:",t)}},fetchSets:async e=>{const o=await fetch(`${S}/Sets`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!o.ok)console.log("fetchSets from Db failed...!!!");else{const t=await o.json();e.commit("addSets",t),console.log("FetchSets from Db done..."),console.log("Sets received:",t)}},fetchWorkouts:async e=>{const o=await fetch(`${S}/Workouts`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!o.ok)console.log("FetchWorkouts from Db failed...!!!");else{const t=await o.json();e.commit("addWorkouts",t),console.log("FetchWorkouts from Db done..."),console.log("Workouts received:",t)}},fetchPrograms:async e=>{const o=await fetch(`${S}/Programs`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!o.ok)console.log("fetchPrograms from Db failed...!!!");else{const t=await o.json();e.commit("addPrograms",t),console.log("FetchPrograms from Db done..."),console.log("programs received:",t)}},fetchProfile:async(e,o)=>{const t=await fetch(`${S}/profile/${o}`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!t.ok)console.log("fetchProfile from Db failed...!!!");else{const l=await t.json();return e.commit("setProfile",l),console.log("FetchProfile from Db done..."),console.log("profile received:",l),l}},fetchUser:async e=>{const o=await fetch(`${S}/user`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}}).catch(t=>{console.log("fetchUser from Db failed, because:",t)});if(o!=null&&!o.ok)console.log(`fetchUser from Db failed...!!! ResponseCode: ${o}`);else{const t=await o.json();return e.commit("setUser",t),console.log("FetchUser from Db done..."),console.log("user received:",t),t}},fetchGoals:async(e,o)=>{const t=[];for(const l of o){const r=await fetch(`${S}/goal/${l}`,{method:"GET",headers:{Authorization:"Bearer "+e.state.token,"Content-Type":"application/json"}});if(!r.ok)console.log(`FetchGoals for GoalId ${l} from Db failed...!!!`);else{const n=await r.json();t.push(n)}}return e.commit("addGoals",t),console.log("FetchGoals from Db done..."),console.log("Goals received:",t),t}},getters:{getExerciseById:e=>o=>e.exercises.find(t=>t.id===o),getSetById:e=>o=>e.sets.find(t=>t.id===o),getWorkoutById:e=>o=>e.workouts.find(t=>t.id===o),getProgramById:e=>o=>e.programs.find(t=>t.id===o),getGoalById:e=>o=>{const t=e.goals.find(l=>l.id===o);return t!=null&&(t.startDate=new Date(t.startDate),t.endDate=new Date(t.endDate)),t}}}),P="/"+window.location.pathname.split("/")[1]+"/";console.log("baseUrl:",P);console.log("store:",G);G.commit("setBaseUrl",P);const Lo=[{path:`${P}/dashboard`,component:Re},{path:"/login",component:Me},{path:`${P}/goals`,component:Ke},{path:`${P}/exercises`,component:$t},{path:`${P}/workouts`,component:Rt},{path:`${P}/programs`,component:_o},{path:`${P}/profile`,component:Uo}],M=re({history:ne(),routes:Lo});M.beforeEach((e,o)=>{console.log(`page redirection occured from: ${o.fullPath} to: ${e.fullPath}`)});const Co={clientId:"MeFit_VA_OH_PP",realm:"MeFit_OH","auth-server-url":"https://mefit-keycloak1.herokuapp.com/auth",url:"https://mefit-keycloak1.herokuapp.com/auth","ssl-required":"external",resource:"MeFit_VA_OH_PP","public-client":!0,"confidential-port":0},H=new ie(Co);H.init({onLoad:"login-required",silentCheckSsoRedirectUri:window.location.origin+"/silent-check-sso.html"}).then(e=>{}).catch(e=>{alert("failed to initialize: "+e),console.log("failed to initialize:",e)});le(Ie,{keycloak:H}).use(G).use(M).mount("#app");