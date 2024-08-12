
<template>
    <div>
        <div class="error"><img src="../assets/error.png">{{msg}} </div>
    </div>
</template>

<script>
 
 export default {
     name:'erroritem',
     props:['msg']

 }

</script>

<style scoped lang="scss">
	.error{
			text-align: center;
			display: flex;
			justify-content: space-around;
			color: white;
			background-color: #e3262a;
			margin: 10px 0px;
			padding: 15px;
			align-items: center;
			border-radius: 5px 15px 5px;
			align-items: center;
		}

		img{
			margin: 5px;
		}

</style>
