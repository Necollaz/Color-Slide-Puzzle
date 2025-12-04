var Deserializers = {}
Deserializers["UnityEngine.JointSpring"] = function (request, data, root) {
  var i380 = root || request.c( 'UnityEngine.JointSpring' )
  var i381 = data
  i380.spring = i381[0]
  i380.damper = i381[1]
  i380.targetPosition = i381[2]
  return i380
}

Deserializers["UnityEngine.JointMotor"] = function (request, data, root) {
  var i382 = root || request.c( 'UnityEngine.JointMotor' )
  var i383 = data
  i382.m_TargetVelocity = i383[0]
  i382.m_Force = i383[1]
  i382.m_FreeSpin = i383[2]
  return i382
}

Deserializers["UnityEngine.JointLimits"] = function (request, data, root) {
  var i384 = root || request.c( 'UnityEngine.JointLimits' )
  var i385 = data
  i384.m_Min = i385[0]
  i384.m_Max = i385[1]
  i384.m_Bounciness = i385[2]
  i384.m_BounceMinVelocity = i385[3]
  i384.m_ContactDistance = i385[4]
  i384.minBounce = i385[5]
  i384.maxBounce = i385[6]
  return i384
}

Deserializers["UnityEngine.JointDrive"] = function (request, data, root) {
  var i386 = root || request.c( 'UnityEngine.JointDrive' )
  var i387 = data
  i386.m_PositionSpring = i387[0]
  i386.m_PositionDamper = i387[1]
  i386.m_MaximumForce = i387[2]
  i386.m_UseAcceleration = i387[3]
  return i386
}

Deserializers["UnityEngine.SoftJointLimitSpring"] = function (request, data, root) {
  var i388 = root || request.c( 'UnityEngine.SoftJointLimitSpring' )
  var i389 = data
  i388.m_Spring = i389[0]
  i388.m_Damper = i389[1]
  return i388
}

Deserializers["UnityEngine.SoftJointLimit"] = function (request, data, root) {
  var i390 = root || request.c( 'UnityEngine.SoftJointLimit' )
  var i391 = data
  i390.m_Limit = i391[0]
  i390.m_Bounciness = i391[1]
  i390.m_ContactDistance = i391[2]
  return i390
}

Deserializers["UnityEngine.WheelFrictionCurve"] = function (request, data, root) {
  var i392 = root || request.c( 'UnityEngine.WheelFrictionCurve' )
  var i393 = data
  i392.m_ExtremumSlip = i393[0]
  i392.m_ExtremumValue = i393[1]
  i392.m_AsymptoteSlip = i393[2]
  i392.m_AsymptoteValue = i393[3]
  i392.m_Stiffness = i393[4]
  return i392
}

Deserializers["UnityEngine.JointAngleLimits2D"] = function (request, data, root) {
  var i394 = root || request.c( 'UnityEngine.JointAngleLimits2D' )
  var i395 = data
  i394.m_LowerAngle = i395[0]
  i394.m_UpperAngle = i395[1]
  return i394
}

Deserializers["UnityEngine.JointMotor2D"] = function (request, data, root) {
  var i396 = root || request.c( 'UnityEngine.JointMotor2D' )
  var i397 = data
  i396.m_MotorSpeed = i397[0]
  i396.m_MaximumMotorTorque = i397[1]
  return i396
}

Deserializers["UnityEngine.JointSuspension2D"] = function (request, data, root) {
  var i398 = root || request.c( 'UnityEngine.JointSuspension2D' )
  var i399 = data
  i398.m_DampingRatio = i399[0]
  i398.m_Frequency = i399[1]
  i398.m_Angle = i399[2]
  return i398
}

Deserializers["UnityEngine.JointTranslationLimits2D"] = function (request, data, root) {
  var i400 = root || request.c( 'UnityEngine.JointTranslationLimits2D' )
  var i401 = data
  i400.m_LowerTranslation = i401[0]
  i400.m_UpperTranslation = i401[1]
  return i400
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Transform"] = function (request, data, root) {
  var i402 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Transform' )
  var i403 = data
  i402.position = new pc.Vec3( i403[0], i403[1], i403[2] )
  i402.scale = new pc.Vec3( i403[3], i403[4], i403[5] )
  i402.rotation = new pc.Quat(i403[6], i403[7], i403[8], i403[9])
  return i402
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshFilter"] = function (request, data, root) {
  var i404 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshFilter' )
  var i405 = data
  request.r(i405[0], i405[1], 0, i404, 'sharedMesh')
  return i404
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.MeshRenderer"] = function (request, data, root) {
  var i406 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.MeshRenderer' )
  var i407 = data
  request.r(i407[0], i407[1], 0, i406, 'additionalVertexStreams')
  i406.enabled = !!i407[2]
  request.r(i407[3], i407[4], 0, i406, 'sharedMaterial')
  var i409 = i407[5]
  var i408 = []
  for(var i = 0; i < i409.length; i += 2) {
  request.r(i409[i + 0], i409[i + 1], 2, i408, '')
  }
  i406.sharedMaterials = i408
  i406.receiveShadows = !!i407[6]
  i406.shadowCastingMode = i407[7]
  i406.sortingLayerID = i407[8]
  i406.sortingOrder = i407[9]
  i406.lightmapIndex = i407[10]
  i406.lightmapSceneIndex = i407[11]
  i406.lightmapScaleOffset = new pc.Vec4( i407[12], i407[13], i407[14], i407[15] )
  i406.lightProbeUsage = i407[16]
  i406.reflectionProbeUsage = i407[17]
  return i406
}

Deserializers["HexCellView"] = function (request, data, root) {
  var i412 = root || request.c( 'HexCellView' )
  var i413 = data
  request.r(i413[0], i413[1], 0, i412, '_renderer')
  return i412
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.GameObject"] = function (request, data, root) {
  var i414 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.GameObject' )
  var i415 = data
  i414.name = i415[0]
  i414.tagId = i415[1]
  i414.enabled = !!i415[2]
  i414.isStatic = !!i415[3]
  i414.layer = i415[4]
  return i414
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh"] = function (request, data, root) {
  var i416 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh' )
  var i417 = data
  i416.name = i417[0]
  i416.halfPrecision = !!i417[1]
  i416.useSimplification = !!i417[2]
  i416.useUInt32IndexFormat = !!i417[3]
  i416.vertexCount = i417[4]
  i416.aabb = i417[5]
  var i419 = i417[6]
  var i418 = []
  for(var i = 0; i < i419.length; i += 1) {
    i418.push( !!i419[i + 0] );
  }
  i416.streams = i418
  i416.vertices = i417[7]
  var i421 = i417[8]
  var i420 = []
  for(var i = 0; i < i421.length; i += 1) {
    i420.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh', i421[i + 0]) );
  }
  i416.subMeshes = i420
  var i423 = i417[9]
  var i422 = []
  for(var i = 0; i < i423.length; i += 16) {
    i422.push( new pc.Mat4().setData(i423[i + 0], i423[i + 1], i423[i + 2], i423[i + 3],  i423[i + 4], i423[i + 5], i423[i + 6], i423[i + 7],  i423[i + 8], i423[i + 9], i423[i + 10], i423[i + 11],  i423[i + 12], i423[i + 13], i423[i + 14], i423[i + 15]) );
  }
  i416.bindposes = i422
  var i425 = i417[10]
  var i424 = []
  for(var i = 0; i < i425.length; i += 1) {
    i424.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape', i425[i + 0]) );
  }
  i416.blendShapes = i424
  return i416
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh"] = function (request, data, root) {
  var i430 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh' )
  var i431 = data
  i430.triangles = i431[0]
  return i430
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape"] = function (request, data, root) {
  var i436 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape' )
  var i437 = data
  i436.name = i437[0]
  var i439 = i437[1]
  var i438 = []
  for(var i = 0; i < i439.length; i += 1) {
    i438.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame', i439[i + 0]) );
  }
  i436.frames = i438
  return i436
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material"] = function (request, data, root) {
  var i440 = root || new pc.UnityMaterial()
  var i441 = data
  i440.name = i441[0]
  request.r(i441[1], i441[2], 0, i440, 'shader')
  i440.renderQueue = i441[3]
  i440.enableInstancing = !!i441[4]
  var i443 = i441[5]
  var i442 = []
  for(var i = 0; i < i443.length; i += 1) {
    i442.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter', i443[i + 0]) );
  }
  i440.floatParameters = i442
  var i445 = i441[6]
  var i444 = []
  for(var i = 0; i < i445.length; i += 1) {
    i444.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter', i445[i + 0]) );
  }
  i440.colorParameters = i444
  var i447 = i441[7]
  var i446 = []
  for(var i = 0; i < i447.length; i += 1) {
    i446.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter', i447[i + 0]) );
  }
  i440.vectorParameters = i446
  var i449 = i441[8]
  var i448 = []
  for(var i = 0; i < i449.length; i += 1) {
    i448.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter', i449[i + 0]) );
  }
  i440.textureParameters = i448
  var i451 = i441[9]
  var i450 = []
  for(var i = 0; i < i451.length; i += 1) {
    i450.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag', i451[i + 0]) );
  }
  i440.materialFlags = i450
  return i440
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter"] = function (request, data, root) {
  var i454 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter' )
  var i455 = data
  i454.name = i455[0]
  i454.value = i455[1]
  return i454
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter"] = function (request, data, root) {
  var i458 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter' )
  var i459 = data
  i458.name = i459[0]
  i458.value = new pc.Color(i459[1], i459[2], i459[3], i459[4])
  return i458
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter"] = function (request, data, root) {
  var i462 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter' )
  var i463 = data
  i462.name = i463[0]
  i462.value = new pc.Vec4( i463[1], i463[2], i463[3], i463[4] )
  return i462
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter"] = function (request, data, root) {
  var i466 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter' )
  var i467 = data
  i466.name = i467[0]
  request.r(i467[1], i467[2], 0, i466, 'value')
  return i466
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag"] = function (request, data, root) {
  var i470 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag' )
  var i471 = data
  i470.name = i471[0]
  i470.enabled = !!i471[1]
  return i470
}

Deserializers["TileStackView"] = function (request, data, root) {
  var i472 = root || request.c( 'TileStackView' )
  var i473 = data
  request.r(i473[0], i473[1], 0, i472, '_renderer')
  return i472
}

Deserializers["TileStackRoot"] = function (request, data, root) {
  var i474 = root || request.c( 'TileStackRoot' )
  var i475 = data
  request.r(i475[0], i475[1], 0, i474, '_tileStackView')
  i474._dragLerpSpeed = i475[2]
  return i474
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.SphereCollider"] = function (request, data, root) {
  var i476 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.SphereCollider' )
  var i477 = data
  i476.center = new pc.Vec3( i477[0], i477[1], i477[2] )
  i476.radius = i477[3]
  i476.enabled = !!i477[4]
  i476.isTrigger = !!i477[5]
  request.r(i477[6], i477[7], 0, i476, 'material')
  return i476
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystem"] = function (request, data, root) {
  var i478 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystem' )
  var i479 = data
  i478.main = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule', i479[0], i478.main)
  i478.colorBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule', i479[1], i478.colorBySpeed)
  i478.colorOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule', i479[2], i478.colorOverLifetime)
  i478.emission = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule', i479[3], i478.emission)
  i478.rotationBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule', i479[4], i478.rotationBySpeed)
  i478.rotationOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule', i479[5], i478.rotationOverLifetime)
  i478.shape = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule', i479[6], i478.shape)
  i478.sizeBySpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule', i479[7], i478.sizeBySpeed)
  i478.sizeOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule', i479[8], i478.sizeOverLifetime)
  i478.textureSheetAnimation = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule', i479[9], i478.textureSheetAnimation)
  i478.velocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule', i479[10], i478.velocityOverLifetime)
  i478.noise = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule', i479[11], i478.noise)
  i478.inheritVelocity = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule', i479[12], i478.inheritVelocity)
  i478.forceOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule', i479[13], i478.forceOverLifetime)
  i478.limitVelocityOverLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule', i479[14], i478.limitVelocityOverLifetime)
  i478.useAutoRandomSeed = !!i479[15]
  i478.randomSeed = i479[16]
  return i478
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule"] = function (request, data, root) {
  var i480 = root || new pc.ParticleSystemMain()
  var i481 = data
  i480.duration = i481[0]
  i480.loop = !!i481[1]
  i480.prewarm = !!i481[2]
  i480.startDelay = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[3], i480.startDelay)
  i480.startLifetime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[4], i480.startLifetime)
  i480.startSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[5], i480.startSpeed)
  i480.startSize3D = !!i481[6]
  i480.startSizeX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[7], i480.startSizeX)
  i480.startSizeY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[8], i480.startSizeY)
  i480.startSizeZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[9], i480.startSizeZ)
  i480.startRotation3D = !!i481[10]
  i480.startRotationX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[11], i480.startRotationX)
  i480.startRotationY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[12], i480.startRotationY)
  i480.startRotationZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[13], i480.startRotationZ)
  i480.startColor = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i481[14], i480.startColor)
  i480.gravityModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i481[15], i480.gravityModifier)
  i480.simulationSpace = i481[16]
  request.r(i481[17], i481[18], 0, i480, 'customSimulationSpace')
  i480.simulationSpeed = i481[19]
  i480.useUnscaledTime = !!i481[20]
  i480.scalingMode = i481[21]
  i480.playOnAwake = !!i481[22]
  i480.maxParticles = i481[23]
  i480.emitterVelocityMode = i481[24]
  i480.stopAction = i481[25]
  return i480
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve"] = function (request, data, root) {
  var i482 = root || new pc.MinMaxCurve()
  var i483 = data
  i482.mode = i483[0]
  i482.curveMin = new pc.AnimationCurve( { keys_flow: i483[1] } )
  i482.curveMax = new pc.AnimationCurve( { keys_flow: i483[2] } )
  i482.curveMultiplier = i483[3]
  i482.constantMin = i483[4]
  i482.constantMax = i483[5]
  return i482
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient"] = function (request, data, root) {
  var i484 = root || new pc.MinMaxGradient()
  var i485 = data
  i484.mode = i485[0]
  i484.gradientMin = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i485[1], i484.gradientMin)
  i484.gradientMax = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient', i485[2], i484.gradientMax)
  i484.colorMin = new pc.Color(i485[3], i485[4], i485[5], i485[6])
  i484.colorMax = new pc.Color(i485[7], i485[8], i485[9], i485[10])
  return i484
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient"] = function (request, data, root) {
  var i486 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient' )
  var i487 = data
  i486.mode = i487[0]
  var i489 = i487[1]
  var i488 = []
  for(var i = 0; i < i489.length; i += 1) {
    i488.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey', i489[i + 0]) );
  }
  i486.colorKeys = i488
  var i491 = i487[2]
  var i490 = []
  for(var i = 0; i < i491.length; i += 1) {
    i490.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey', i491[i + 0]) );
  }
  i486.alphaKeys = i490
  return i486
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule"] = function (request, data, root) {
  var i492 = root || new pc.ParticleSystemColorBySpeed()
  var i493 = data
  i492.enabled = !!i493[0]
  i492.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i493[1], i492.color)
  i492.range = new pc.Vec2( i493[2], i493[3] )
  return i492
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey"] = function (request, data, root) {
  var i496 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey' )
  var i497 = data
  i496.color = new pc.Color(i497[0], i497[1], i497[2], i497[3])
  i496.time = i497[4]
  return i496
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey"] = function (request, data, root) {
  var i500 = root || request.c( 'Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey' )
  var i501 = data
  i500.alpha = i501[0]
  i500.time = i501[1]
  return i500
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule"] = function (request, data, root) {
  var i502 = root || new pc.ParticleSystemColorOverLifetime()
  var i503 = data
  i502.enabled = !!i503[0]
  i502.color = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient', i503[1], i502.color)
  return i502
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule"] = function (request, data, root) {
  var i504 = root || new pc.ParticleSystemEmitter()
  var i505 = data
  i504.enabled = !!i505[0]
  i504.rateOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i505[1], i504.rateOverTime)
  i504.rateOverDistance = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i505[2], i504.rateOverDistance)
  var i507 = i505[3]
  var i506 = []
  for(var i = 0; i < i507.length; i += 1) {
    i506.push( request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst', i507[i + 0]) );
  }
  i504.bursts = i506
  return i504
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst"] = function (request, data, root) {
  var i510 = root || new pc.ParticleSystemBurst()
  var i511 = data
  i510.count = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i511[0], i510.count)
  i510.cycleCount = i511[1]
  i510.minCount = i511[2]
  i510.maxCount = i511[3]
  i510.repeatInterval = i511[4]
  i510.time = i511[5]
  return i510
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule"] = function (request, data, root) {
  var i512 = root || new pc.ParticleSystemRotationBySpeed()
  var i513 = data
  i512.enabled = !!i513[0]
  i512.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[1], i512.x)
  i512.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[2], i512.y)
  i512.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i513[3], i512.z)
  i512.separateAxes = !!i513[4]
  i512.range = new pc.Vec2( i513[5], i513[6] )
  return i512
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule"] = function (request, data, root) {
  var i514 = root || new pc.ParticleSystemRotationOverLifetime()
  var i515 = data
  i514.enabled = !!i515[0]
  i514.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[1], i514.x)
  i514.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[2], i514.y)
  i514.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i515[3], i514.z)
  i514.separateAxes = !!i515[4]
  return i514
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule"] = function (request, data, root) {
  var i516 = root || new pc.ParticleSystemShape()
  var i517 = data
  i516.enabled = !!i517[0]
  i516.shapeType = i517[1]
  i516.randomDirectionAmount = i517[2]
  i516.sphericalDirectionAmount = i517[3]
  i516.randomPositionAmount = i517[4]
  i516.alignToDirection = !!i517[5]
  i516.radius = i517[6]
  i516.radiusMode = i517[7]
  i516.radiusSpread = i517[8]
  i516.radiusSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i517[9], i516.radiusSpeed)
  i516.radiusThickness = i517[10]
  i516.angle = i517[11]
  i516.length = i517[12]
  i516.boxThickness = new pc.Vec3( i517[13], i517[14], i517[15] )
  i516.meshShapeType = i517[16]
  request.r(i517[17], i517[18], 0, i516, 'mesh')
  request.r(i517[19], i517[20], 0, i516, 'meshRenderer')
  request.r(i517[21], i517[22], 0, i516, 'skinnedMeshRenderer')
  i516.useMeshMaterialIndex = !!i517[23]
  i516.meshMaterialIndex = i517[24]
  i516.useMeshColors = !!i517[25]
  i516.normalOffset = i517[26]
  i516.arc = i517[27]
  i516.arcMode = i517[28]
  i516.arcSpread = i517[29]
  i516.arcSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i517[30], i516.arcSpeed)
  i516.donutRadius = i517[31]
  i516.position = new pc.Vec3( i517[32], i517[33], i517[34] )
  i516.rotation = new pc.Vec3( i517[35], i517[36], i517[37] )
  i516.scale = new pc.Vec3( i517[38], i517[39], i517[40] )
  return i516
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule"] = function (request, data, root) {
  var i518 = root || new pc.ParticleSystemSizeBySpeed()
  var i519 = data
  i518.enabled = !!i519[0]
  i518.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i519[1], i518.x)
  i518.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i519[2], i518.y)
  i518.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i519[3], i518.z)
  i518.separateAxes = !!i519[4]
  i518.range = new pc.Vec2( i519[5], i519[6] )
  return i518
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule"] = function (request, data, root) {
  var i520 = root || new pc.ParticleSystemSizeOverLifetime()
  var i521 = data
  i520.enabled = !!i521[0]
  i520.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i521[1], i520.x)
  i520.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i521[2], i520.y)
  i520.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i521[3], i520.z)
  i520.separateAxes = !!i521[4]
  return i520
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule"] = function (request, data, root) {
  var i522 = root || new pc.ParticleSystemTextureSheetAnimation()
  var i523 = data
  i522.enabled = !!i523[0]
  i522.mode = i523[1]
  i522.animation = i523[2]
  i522.numTilesX = i523[3]
  i522.numTilesY = i523[4]
  i522.useRandomRow = !!i523[5]
  i522.frameOverTime = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i523[6], i522.frameOverTime)
  i522.startFrame = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i523[7], i522.startFrame)
  i522.cycleCount = i523[8]
  i522.rowIndex = i523[9]
  i522.flipU = i523[10]
  i522.flipV = i523[11]
  i522.spriteCount = i523[12]
  var i525 = i523[13]
  var i524 = []
  for(var i = 0; i < i525.length; i += 2) {
  request.r(i525[i + 0], i525[i + 1], 2, i524, '')
  }
  i522.sprites = i524
  return i522
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule"] = function (request, data, root) {
  var i528 = root || new pc.ParticleSystemVelocityOverLifetime()
  var i529 = data
  i528.enabled = !!i529[0]
  i528.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[1], i528.x)
  i528.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[2], i528.y)
  i528.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[3], i528.z)
  i528.radial = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[4], i528.radial)
  i528.speedModifier = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[5], i528.speedModifier)
  i528.space = i529[6]
  i528.orbitalX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[7], i528.orbitalX)
  i528.orbitalY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[8], i528.orbitalY)
  i528.orbitalZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[9], i528.orbitalZ)
  i528.orbitalOffsetX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[10], i528.orbitalOffsetX)
  i528.orbitalOffsetY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[11], i528.orbitalOffsetY)
  i528.orbitalOffsetZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i529[12], i528.orbitalOffsetZ)
  return i528
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule"] = function (request, data, root) {
  var i530 = root || new pc.ParticleSystemNoise()
  var i531 = data
  i530.enabled = !!i531[0]
  i530.separateAxes = !!i531[1]
  i530.strengthX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[2], i530.strengthX)
  i530.strengthY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[3], i530.strengthY)
  i530.strengthZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[4], i530.strengthZ)
  i530.frequency = i531[5]
  i530.damping = !!i531[6]
  i530.octaveCount = i531[7]
  i530.octaveMultiplier = i531[8]
  i530.octaveScale = i531[9]
  i530.quality = i531[10]
  i530.scrollSpeed = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[11], i530.scrollSpeed)
  i530.scrollSpeedMultiplier = i531[12]
  i530.remapEnabled = !!i531[13]
  i530.remapX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[14], i530.remapX)
  i530.remapY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[15], i530.remapY)
  i530.remapZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[16], i530.remapZ)
  i530.positionAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[17], i530.positionAmount)
  i530.rotationAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[18], i530.rotationAmount)
  i530.sizeAmount = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i531[19], i530.sizeAmount)
  return i530
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule"] = function (request, data, root) {
  var i532 = root || new pc.ParticleSystemInheritVelocity()
  var i533 = data
  i532.enabled = !!i533[0]
  i532.mode = i533[1]
  i532.curve = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i533[2], i532.curve)
  return i532
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule"] = function (request, data, root) {
  var i534 = root || new pc.ParticleSystemForceOverLifetime()
  var i535 = data
  i534.enabled = !!i535[0]
  i534.x = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i535[1], i534.x)
  i534.y = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i535[2], i534.y)
  i534.z = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i535[3], i534.z)
  i534.space = i535[4]
  i534.randomized = !!i535[5]
  return i534
}

Deserializers["Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule"] = function (request, data, root) {
  var i536 = root || new pc.ParticleSystemLimitVelocityOverLifetime()
  var i537 = data
  i536.enabled = !!i537[0]
  i536.limit = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i537[1], i536.limit)
  i536.limitX = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i537[2], i536.limitX)
  i536.limitY = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i537[3], i536.limitY)
  i536.limitZ = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i537[4], i536.limitZ)
  i536.dampen = i537[5]
  i536.separateAxes = !!i537[6]
  i536.space = i537[7]
  i536.drag = request.d('Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve', i537[8], i536.drag)
  i536.multiplyDragByParticleSize = !!i537[9]
  i536.multiplyDragByParticleVelocity = !!i537[10]
  return i536
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer"] = function (request, data, root) {
  var i538 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer' )
  var i539 = data
  request.r(i539[0], i539[1], 0, i538, 'mesh')
  i538.meshCount = i539[2]
  i538.activeVertexStreamsCount = i539[3]
  i538.alignment = i539[4]
  i538.renderMode = i539[5]
  i538.sortMode = i539[6]
  i538.lengthScale = i539[7]
  i538.velocityScale = i539[8]
  i538.cameraVelocityScale = i539[9]
  i538.normalDirection = i539[10]
  i538.sortingFudge = i539[11]
  i538.minParticleSize = i539[12]
  i538.maxParticleSize = i539[13]
  i538.pivot = new pc.Vec3( i539[14], i539[15], i539[16] )
  request.r(i539[17], i539[18], 0, i538, 'trailMaterial')
  i538.applyActiveColorSpace = !!i539[19]
  i538.enabled = !!i539[20]
  request.r(i539[21], i539[22], 0, i538, 'sharedMaterial')
  var i541 = i539[23]
  var i540 = []
  for(var i = 0; i < i541.length; i += 2) {
  request.r(i541[i + 0], i541[i + 1], 2, i540, '')
  }
  i538.sharedMaterials = i540
  i538.receiveShadows = !!i539[24]
  i538.shadowCastingMode = i539[25]
  i538.sortingLayerID = i539[26]
  i538.sortingOrder = i539[27]
  i538.lightmapIndex = i539[28]
  i538.lightmapSceneIndex = i539[29]
  i538.lightmapScaleOffset = new pc.Vec4( i539[30], i539[31], i539[32], i539[33] )
  i538.lightProbeUsage = i539[34]
  i538.reflectionProbeUsage = i539[35]
  return i538
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Texture2D"] = function (request, data, root) {
  var i542 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Texture2D' )
  var i543 = data
  i542.name = i543[0]
  i542.width = i543[1]
  i542.height = i543[2]
  i542.mipmapCount = i543[3]
  i542.anisoLevel = i543[4]
  i542.filterMode = i543[5]
  i542.hdr = !!i543[6]
  i542.format = i543[7]
  i542.wrapMode = i543[8]
  i542.alphaIsTransparency = !!i543[9]
  i542.alphaSource = i543[10]
  i542.graphicsFormat = i543[11]
  i542.sRGBTexture = !!i543[12]
  i542.desiredColorSpace = i543[13]
  i542.wrapU = i543[14]
  i542.wrapV = i543[15]
  return i542
}

Deserializers["Luna.Unity.DTO.UnityEngine.Textures.Cubemap"] = function (request, data, root) {
  var i544 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Textures.Cubemap' )
  var i545 = data
  i544.name = i545[0]
  i544.atlasId = i545[1]
  i544.mipmapCount = i545[2]
  i544.hdr = !!i545[3]
  i544.size = i545[4]
  i544.anisoLevel = i545[5]
  i544.filterMode = i545[6]
  var i547 = i545[7]
  var i546 = []
  for(var i = 0; i < i547.length; i += 4) {
    i546.push( UnityEngine.Rect.MinMaxRect(i547[i + 0], i547[i + 1], i547[i + 2], i547[i + 3]) );
  }
  i544.rects = i546
  i544.wrapU = i545[8]
  i544.wrapV = i545[9]
  return i544
}

Deserializers["Luna.Unity.DTO.UnityEngine.Scene.Scene"] = function (request, data, root) {
  var i550 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Scene.Scene' )
  var i551 = data
  i550.name = i551[0]
  i550.index = i551[1]
  i550.startup = !!i551[2]
  return i550
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Camera"] = function (request, data, root) {
  var i552 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Camera' )
  var i553 = data
  i552.aspect = i553[0]
  i552.orthographic = !!i553[1]
  i552.orthographicSize = i553[2]
  i552.backgroundColor = new pc.Color(i553[3], i553[4], i553[5], i553[6])
  i552.nearClipPlane = i553[7]
  i552.farClipPlane = i553[8]
  i552.fieldOfView = i553[9]
  i552.depth = i553[10]
  i552.clearFlags = i553[11]
  i552.cullingMask = i553[12]
  i552.rect = i553[13]
  request.r(i553[14], i553[15], 0, i552, 'targetTexture')
  i552.usePhysicalProperties = !!i553[16]
  i552.focalLength = i553[17]
  i552.sensorSize = new pc.Vec2( i553[18], i553[19] )
  i552.lensShift = new pc.Vec2( i553[20], i553[21] )
  i552.gateFit = i553[22]
  i552.commandBufferCount = i553[23]
  i552.cameraType = i553[24]
  i552.enabled = !!i553[25]
  return i552
}

Deserializers["Luna.Unity.DTO.UnityEngine.Components.Light"] = function (request, data, root) {
  var i554 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Components.Light' )
  var i555 = data
  i554.type = i555[0]
  i554.color = new pc.Color(i555[1], i555[2], i555[3], i555[4])
  i554.cullingMask = i555[5]
  i554.intensity = i555[6]
  i554.range = i555[7]
  i554.spotAngle = i555[8]
  i554.shadows = i555[9]
  i554.shadowNormalBias = i555[10]
  i554.shadowBias = i555[11]
  i554.shadowStrength = i555[12]
  i554.shadowResolution = i555[13]
  i554.lightmapBakeType = i555[14]
  i554.renderMode = i555[15]
  request.r(i555[16], i555[17], 0, i554, 'cookie')
  i554.cookieSize = i555[18]
  i554.shadowNearPlane = i555[19]
  i554.enabled = !!i555[20]
  return i554
}

Deserializers["UnityEngine.Rendering.Universal.UniversalAdditionalLightData"] = function (request, data, root) {
  var i556 = root || request.c( 'UnityEngine.Rendering.Universal.UniversalAdditionalLightData' )
  var i557 = data
  i556.m_Version = i557[0]
  i556.m_UsePipelineSettings = !!i557[1]
  i556.m_AdditionalLightsShadowResolutionTier = i557[2]
  i556.m_LightLayerMask = i557[3]
  i556.m_RenderingLayers = i557[4]
  i556.m_CustomShadowLayers = !!i557[5]
  i556.m_ShadowLayerMask = i557[6]
  i556.m_ShadowRenderingLayers = i557[7]
  i556.m_LightCookieSize = new pc.Vec2( i557[8], i557[9] )
  i556.m_LightCookieOffset = new pc.Vec2( i557[10], i557[11] )
  i556.m_SoftShadowQuality = i557[12]
  return i556
}

Deserializers["Zenject.SceneContext"] = function (request, data, root) {
  var i558 = root || request.c( 'Zenject.SceneContext' )
  var i559 = data
  i558.OnPreInstall = request.d('UnityEngine.Events.UnityEvent', i559[0], i558.OnPreInstall)
  i558.OnPostInstall = request.d('UnityEngine.Events.UnityEvent', i559[1], i558.OnPostInstall)
  i558.OnPreResolve = request.d('UnityEngine.Events.UnityEvent', i559[2], i558.OnPreResolve)
  i558.OnPostResolve = request.d('UnityEngine.Events.UnityEvent', i559[3], i558.OnPostResolve)
  i558._parentNewObjectsUnderSceneContext = !!i559[4]
  var i561 = i559[5]
  var i560 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i561.length; i += 1) {
    i560.add(i561[i + 0]);
  }
  i558._contractNames = i560
  var i563 = i559[6]
  var i562 = new (System.Collections.Generic.List$1(Bridge.ns('System.String')))
  for(var i = 0; i < i563.length; i += 1) {
    i562.add(i563[i + 0]);
  }
  i558._parentContractNames = i562
  i558._autoRun = !!i559[7]
  var i565 = i559[8]
  var i564 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.ScriptableObjectInstaller')))
  for(var i = 0; i < i565.length; i += 2) {
  request.r(i565[i + 0], i565[i + 1], 1, i564, '')
  }
  i558._scriptableObjectInstallers = i564
  var i567 = i559[9]
  var i566 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.MonoInstaller')))
  for(var i = 0; i < i567.length; i += 2) {
  request.r(i567[i + 0], i567[i + 1], 1, i566, '')
  }
  i558._monoInstallers = i566
  var i569 = i559[10]
  var i568 = new (System.Collections.Generic.List$1(Bridge.ns('Zenject.MonoInstaller')))
  for(var i = 0; i < i569.length; i += 2) {
  request.r(i569[i + 0], i569[i + 1], 1, i568, '')
  }
  i558._installerPrefabs = i568
  return i558
}

Deserializers["UnityEngine.Events.UnityEvent"] = function (request, data, root) {
  var i570 = root || request.c( 'UnityEngine.Events.UnityEvent' )
  var i571 = data
  i570.m_PersistentCalls = request.d('UnityEngine.Events.PersistentCallGroup', i571[0], i570.m_PersistentCalls)
  return i570
}

Deserializers["UnityEngine.Events.PersistentCallGroup"] = function (request, data, root) {
  var i572 = root || request.c( 'UnityEngine.Events.PersistentCallGroup' )
  var i573 = data
  var i575 = i573[0]
  var i574 = new (System.Collections.Generic.List$1(Bridge.ns('UnityEngine.Events.PersistentCall')))
  for(var i = 0; i < i575.length; i += 1) {
    i574.add(request.d('UnityEngine.Events.PersistentCall', i575[i + 0]));
  }
  i572.m_Calls = i574
  return i572
}

Deserializers["UnityEngine.Events.PersistentCall"] = function (request, data, root) {
  var i578 = root || request.c( 'UnityEngine.Events.PersistentCall' )
  var i579 = data
  request.r(i579[0], i579[1], 0, i578, 'm_Target')
  i578.m_TargetAssemblyTypeName = i579[2]
  i578.m_MethodName = i579[3]
  i578.m_Mode = i579[4]
  i578.m_Arguments = request.d('UnityEngine.Events.ArgumentCache', i579[5], i578.m_Arguments)
  i578.m_CallState = i579[6]
  return i578
}

Deserializers["GameplayInstaller"] = function (request, data, root) {
  var i586 = root || request.c( 'GameplayInstaller' )
  var i587 = data
  request.r(i587[0], i587[1], 0, i586, '_grid')
  request.r(i587[2], i587[3], 0, i586, '_cellPrefab')
  request.r(i587[4], i587[5], 0, i586, '_cellStackPrefab')
  request.r(i587[6], i587[7], 0, i586, '_spawnStackRootPrefab')
  request.r(i587[8], i587[9], 0, i586, '_tileConfig')
  request.r(i587[10], i587[11], 0, i586, '_gridDefinitionConfig')
  return i586
}

Deserializers["TileStackDragInput"] = function (request, data, root) {
  var i588 = root || request.c( 'TileStackDragInput' )
  var i589 = data
  request.r(i589[0], i589[1], 0, i588, '_camera')
  i588._stackLayerMask = UnityEngine.LayerMask.FromIntegerValue( i589[2] )
  i588._maxCellPickDistance = i589[3]
  return i588
}

Deserializers["TileStackSpawnPoint"] = function (request, data, root) {
  var i590 = root || request.c( 'TileStackSpawnPoint' )
  var i591 = data
  return i590
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings"] = function (request, data, root) {
  var i592 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings' )
  var i593 = data
  i592.ambientIntensity = i593[0]
  i592.reflectionIntensity = i593[1]
  i592.ambientMode = i593[2]
  i592.ambientLight = new pc.Color(i593[3], i593[4], i593[5], i593[6])
  i592.ambientSkyColor = new pc.Color(i593[7], i593[8], i593[9], i593[10])
  i592.ambientGroundColor = new pc.Color(i593[11], i593[12], i593[13], i593[14])
  i592.ambientEquatorColor = new pc.Color(i593[15], i593[16], i593[17], i593[18])
  i592.fogColor = new pc.Color(i593[19], i593[20], i593[21], i593[22])
  i592.fogEndDistance = i593[23]
  i592.fogStartDistance = i593[24]
  i592.fogDensity = i593[25]
  i592.fog = !!i593[26]
  request.r(i593[27], i593[28], 0, i592, 'skybox')
  i592.fogMode = i593[29]
  var i595 = i593[30]
  var i594 = []
  for(var i = 0; i < i595.length; i += 1) {
    i594.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap', i595[i + 0]) );
  }
  i592.lightmaps = i594
  i592.lightProbes = request.d('Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes', i593[31], i592.lightProbes)
  i592.lightmapsMode = i593[32]
  i592.mixedBakeMode = i593[33]
  i592.environmentLightingMode = i593[34]
  i592.ambientProbe = new pc.SphericalHarmonicsL2(i593[35])
  i592.referenceAmbientProbe = new pc.SphericalHarmonicsL2(i593[36])
  i592.useReferenceAmbientProbe = !!i593[37]
  request.r(i593[38], i593[39], 0, i592, 'customReflection')
  request.r(i593[40], i593[41], 0, i592, 'defaultReflection')
  i592.defaultReflectionMode = i593[42]
  i592.defaultReflectionResolution = i593[43]
  i592.sunLightObjectId = i593[44]
  i592.pixelLightCount = i593[45]
  i592.defaultReflectionHDR = !!i593[46]
  i592.hasLightDataAsset = !!i593[47]
  i592.hasManualGenerate = !!i593[48]
  return i592
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap"] = function (request, data, root) {
  var i598 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap' )
  var i599 = data
  request.r(i599[0], i599[1], 0, i598, 'lightmapColor')
  request.r(i599[2], i599[3], 0, i598, 'lightmapDirection')
  return i598
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes"] = function (request, data, root) {
  var i600 = root || new UnityEngine.LightProbes()
  var i601 = data
  return i600
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset"] = function (request, data, root) {
  var i608 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset' )
  var i609 = data
  i608.AdditionalLightsPerObjectLimit = i609[0]
  i608.AdditionalLightsRenderingMode = i609[1]
  i608.LightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i609[2], i608.LightRenderingMode)
  i608.ColorGradingLutSize = i609[3]
  i608.ColorGradingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode', i609[4], i608.ColorGradingMode)
  i608.MainLightRenderingMode = request.d('Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode', i609[5], i608.MainLightRenderingMode)
  i608.MainLightRenderingModeValue = i609[6]
  i608.SupportsMainLightShadows = !!i609[7]
  i608.MixedLightingSupported = !!i609[8]
  i608.MsaaQuality = request.d('Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality', i609[9], i608.MsaaQuality)
  i608.MSAA = i609[10]
  i608.OpaqueDownsampling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Downsampling', i609[11], i608.OpaqueDownsampling)
  i608.MainLightShadowmapResolution = request.d('Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution', i609[12], i608.MainLightShadowmapResolution)
  i608.MainLightShadowmapResolutionValue = i609[13]
  i608.SupportsSoftShadows = !!i609[14]
  i608.SoftShadowQuality = request.d('Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality', i609[15], i608.SoftShadowQuality)
  i608.SoftShadowQualityValue = i609[16]
  i608.ShadowDistance = i609[17]
  i608.ShadowCascadeCount = i609[18]
  i608.Cascade2Split = i609[19]
  i608.Cascade3Split = new pc.Vec2( i609[20], i609[21] )
  i608.Cascade4Split = new pc.Vec3( i609[22], i609[23], i609[24] )
  i608.CascadeBorder = i609[25]
  i608.ShadowDepthBias = i609[26]
  i608.ShadowNormalBias = i609[27]
  i608.RenderScale = i609[28]
  i608.RequireDepthTexture = !!i609[29]
  i608.RequireOpaqueTexture = !!i609[30]
  i608.SupportsHDR = !!i609[31]
  i608.SupportsTerrainHoles = !!i609[32]
  return i608
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode"] = function (request, data, root) {
  var i610 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode' )
  var i611 = data
  i610.Disabled = i611[0]
  i610.PerVertex = i611[1]
  i610.PerPixel = i611[2]
  return i610
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode"] = function (request, data, root) {
  var i612 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode' )
  var i613 = data
  i612.LowDynamicRange = i613[0]
  i612.HighDynamicRange = i613[1]
  return i612
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality"] = function (request, data, root) {
  var i614 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality' )
  var i615 = data
  i614.Disabled = i615[0]
  i614._2x = i615[1]
  i614._4x = i615[2]
  i614._8x = i615[3]
  return i614
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Downsampling"] = function (request, data, root) {
  var i616 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Downsampling' )
  var i617 = data
  i616.None = i617[0]
  i616._2xBilinear = i617[1]
  i616._4xBox = i617[2]
  i616._4xBilinear = i617[3]
  return i616
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution"] = function (request, data, root) {
  var i618 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution' )
  var i619 = data
  i618._256 = i619[0]
  i618._512 = i619[1]
  i618._1024 = i619[2]
  i618._2048 = i619[3]
  i618._4096 = i619[4]
  return i618
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality"] = function (request, data, root) {
  var i620 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality' )
  var i621 = data
  i620.UsePipelineSettings = i621[0]
  i620.Low = i621[1]
  i620.Medium = i621[2]
  i620.High = i621[3]
  return i620
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader"] = function (request, data, root) {
  var i622 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader' )
  var i623 = data
  var i625 = i623[0]
  var i624 = new (System.Collections.Generic.List$1(Bridge.ns('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError')))
  for(var i = 0; i < i625.length; i += 1) {
    i624.add(request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError', i625[i + 0]));
  }
  i622.ShaderCompilationErrors = i624
  i622.name = i623[1]
  i622.guid = i623[2]
  var i627 = i623[3]
  var i626 = []
  for(var i = 0; i < i627.length; i += 1) {
    i626.push( i627[i + 0] );
  }
  i622.shaderDefinedKeywords = i626
  var i629 = i623[4]
  var i628 = []
  for(var i = 0; i < i629.length; i += 1) {
    i628.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass', i629[i + 0]) );
  }
  i622.passes = i628
  var i631 = i623[5]
  var i630 = []
  for(var i = 0; i < i631.length; i += 1) {
    i630.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass', i631[i + 0]) );
  }
  i622.usePasses = i630
  var i633 = i623[6]
  var i632 = []
  for(var i = 0; i < i633.length; i += 1) {
    i632.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue', i633[i + 0]) );
  }
  i622.defaultParameterValues = i632
  request.r(i623[7], i623[8], 0, i622, 'unityFallbackShader')
  i622.readDepth = !!i623[9]
  i622.hasDepthOnlyPass = !!i623[10]
  i622.isCreatedByShaderGraph = !!i623[11]
  i622.disableBatching = !!i623[12]
  i622.compiled = !!i623[13]
  return i622
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError"] = function (request, data, root) {
  var i636 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError' )
  var i637 = data
  i636.shaderName = i637[0]
  i636.errorMessage = i637[1]
  return i636
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass"] = function (request, data, root) {
  var i642 = root || new pc.UnityShaderPass()
  var i643 = data
  i642.id = i643[0]
  i642.subShaderIndex = i643[1]
  i642.name = i643[2]
  i642.passType = i643[3]
  i642.grabPassTextureName = i643[4]
  i642.usePass = !!i643[5]
  i642.zTest = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[6], i642.zTest)
  i642.zWrite = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[7], i642.zWrite)
  i642.culling = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[8], i642.culling)
  i642.blending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i643[9], i642.blending)
  i642.alphaBlending = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending', i643[10], i642.alphaBlending)
  i642.colorWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[11], i642.colorWriteMask)
  i642.offsetUnits = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[12], i642.offsetUnits)
  i642.offsetFactor = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[13], i642.offsetFactor)
  i642.stencilRef = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[14], i642.stencilRef)
  i642.stencilReadMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[15], i642.stencilReadMask)
  i642.stencilWriteMask = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i643[16], i642.stencilWriteMask)
  i642.stencilOp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i643[17], i642.stencilOp)
  i642.stencilOpFront = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i643[18], i642.stencilOpFront)
  i642.stencilOpBack = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp', i643[19], i642.stencilOpBack)
  var i645 = i643[20]
  var i644 = []
  for(var i = 0; i < i645.length; i += 1) {
    i644.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag', i645[i + 0]) );
  }
  i642.tags = i644
  var i647 = i643[21]
  var i646 = []
  for(var i = 0; i < i647.length; i += 1) {
    i646.push( i647[i + 0] );
  }
  i642.passDefinedKeywords = i646
  var i649 = i643[22]
  var i648 = []
  for(var i = 0; i < i649.length; i += 1) {
    i648.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup', i649[i + 0]) );
  }
  i642.passDefinedKeywordGroups = i648
  var i651 = i643[23]
  var i650 = []
  for(var i = 0; i < i651.length; i += 1) {
    i650.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i651[i + 0]) );
  }
  i642.variants = i650
  var i653 = i643[24]
  var i652 = []
  for(var i = 0; i < i653.length; i += 1) {
    i652.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant', i653[i + 0]) );
  }
  i642.excludedVariants = i652
  i642.hasDepthReader = !!i643[25]
  return i642
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value"] = function (request, data, root) {
  var i654 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value' )
  var i655 = data
  i654.val = i655[0]
  i654.name = i655[1]
  return i654
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending"] = function (request, data, root) {
  var i656 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending' )
  var i657 = data
  i656.src = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i657[0], i656.src)
  i656.dst = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i657[1], i656.dst)
  i656.op = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i657[2], i656.op)
  return i656
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp"] = function (request, data, root) {
  var i658 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp' )
  var i659 = data
  i658.pass = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i659[0], i658.pass)
  i658.fail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i659[1], i658.fail)
  i658.zFail = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i659[2], i658.zFail)
  i658.comp = request.d('Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value', i659[3], i658.comp)
  return i658
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag"] = function (request, data, root) {
  var i662 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag' )
  var i663 = data
  i662.name = i663[0]
  i662.value = i663[1]
  return i662
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup"] = function (request, data, root) {
  var i666 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup' )
  var i667 = data
  var i669 = i667[0]
  var i668 = []
  for(var i = 0; i < i669.length; i += 1) {
    i668.push( i669[i + 0] );
  }
  i666.keywords = i668
  i666.hasDiscard = !!i667[1]
  return i666
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant"] = function (request, data, root) {
  var i672 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant' )
  var i673 = data
  i672.passId = i673[0]
  i672.subShaderIndex = i673[1]
  var i675 = i673[2]
  var i674 = []
  for(var i = 0; i < i675.length; i += 1) {
    i674.push( i675[i + 0] );
  }
  i672.keywords = i674
  i672.vertexProgram = i673[3]
  i672.fragmentProgram = i673[4]
  i672.exportedForWebGl2 = !!i673[5]
  i672.readDepth = !!i673[6]
  return i672
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass"] = function (request, data, root) {
  var i678 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass' )
  var i679 = data
  request.r(i679[0], i679[1], 0, i678, 'shader')
  i678.pass = i679[2]
  return i678
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue"] = function (request, data, root) {
  var i682 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue' )
  var i683 = data
  i682.name = i683[0]
  i682.type = i683[1]
  i682.value = new pc.Vec4( i683[2], i683[3], i683[4], i683[5] )
  i682.textureValue = i683[6]
  i682.shaderPropertyFlag = i683[7]
  return i682
}

Deserializers["TileConfig"] = function (request, data, root) {
  var i684 = root || request.c( 'TileConfig' )
  var i685 = data
  var i687 = i685[0]
  var i686 = []
  for(var i = 0; i < i687.length; i += 4) {
    i686.push( new pc.Color(i687[i + 0], i687[i + 1], i687[i + 2], i687[i + 3]) );
  }
  i684._colors = i686
  i684._maxStackSize = i685[1]
  i684._minGeneratedStackSize = i685[2]
  i684._maxGeneratedStackSize = i685[3]
  i684._minClusterSize = i685[4]
  i684._maxClusterSize = i685[5]
  i684._minColorBlocksPerStack = i685[6]
  i684._maxColorBlocksPerStack = i685[7]
  i684._maxColorPickAttempts = i685[8]
  i684._minTilesPerColorBlock = i685[9]
  i684._minTilesPerColorSpawn = i685[10]
  i684._maxTilesPerColorSpawn = i685[11]
  i684._maxStackDistributionIterations = i685[12]
  i684._maxColorsPerSpawnStack = i685[13]
  i684._oneColorStackWeightPercent = i685[14]
  i684._twoColorsStackWeightPercent = i685[15]
  i684._threeColorsStackWeightPercent = i685[16]
  i684._xzScaleFactor = i685[17]
  i684._stackHeight = i685[18]
  i684._yOffset = i685[19]
  i684._spawnXzWorldSize = i685[20]
  i684._spawnStackWorldHeight = i685[21]
  i684._spawnYOffset = i685[22]
  i684._segmentHeight = i685[23]
  i684._segmentGap = i685[24]
  request.r(i685[25], i685[26], 0, i684, '_tileClearEffectPrefab')
  request.r(i685[27], i685[28], 0, i684, '_stackClearEffectPrefab')
  i684._tileClearEffectPrewarmCount = i685[29]
  i684._stackClearEffectPrewarmCount = i685[30]
  return i684
}

Deserializers["GridDefinitionConfig"] = function (request, data, root) {
  var i690 = root || request.c( 'GridDefinitionConfig' )
  var i691 = data
  i690._shapeType = i691[0]
  i690._width = i691[1]
  i690._height = i691[2]
  i690._hexRadius = i691[3]
  i690._occupiedCellsCount = i691[4]
  return i690
}

Deserializers["DG.Tweening.Core.DOTweenSettings"] = function (request, data, root) {
  var i692 = root || request.c( 'DG.Tweening.Core.DOTweenSettings' )
  var i693 = data
  i692.useSafeMode = !!i693[0]
  i692.safeModeOptions = request.d('DG.Tweening.Core.DOTweenSettings+SafeModeOptions', i693[1], i692.safeModeOptions)
  i692.timeScale = i693[2]
  i692.unscaledTimeScale = i693[3]
  i692.useSmoothDeltaTime = !!i693[4]
  i692.maxSmoothUnscaledTime = i693[5]
  i692.rewindCallbackMode = i693[6]
  i692.showUnityEditorReport = !!i693[7]
  i692.logBehaviour = i693[8]
  i692.drawGizmos = !!i693[9]
  i692.defaultRecyclable = !!i693[10]
  i692.defaultAutoPlay = i693[11]
  i692.defaultUpdateType = i693[12]
  i692.defaultTimeScaleIndependent = !!i693[13]
  i692.defaultEaseType = i693[14]
  i692.defaultEaseOvershootOrAmplitude = i693[15]
  i692.defaultEasePeriod = i693[16]
  i692.defaultAutoKill = !!i693[17]
  i692.defaultLoopType = i693[18]
  i692.debugMode = !!i693[19]
  i692.debugStoreTargetId = !!i693[20]
  i692.showPreviewPanel = !!i693[21]
  i692.storeSettingsLocation = i693[22]
  i692.modules = request.d('DG.Tweening.Core.DOTweenSettings+ModulesSetup', i693[23], i692.modules)
  i692.createASMDEF = !!i693[24]
  i692.showPlayingTweens = !!i693[25]
  i692.showPausedTweens = !!i693[26]
  return i692
}

Deserializers["DG.Tweening.Core.DOTweenSettings+SafeModeOptions"] = function (request, data, root) {
  var i694 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+SafeModeOptions' )
  var i695 = data
  i694.logBehaviour = i695[0]
  i694.nestedTweenFailureBehaviour = i695[1]
  return i694
}

Deserializers["DG.Tweening.Core.DOTweenSettings+ModulesSetup"] = function (request, data, root) {
  var i696 = root || request.c( 'DG.Tweening.Core.DOTweenSettings+ModulesSetup' )
  var i697 = data
  i696.showPanel = !!i697[0]
  i696.audioEnabled = !!i697[1]
  i696.physicsEnabled = !!i697[2]
  i696.physics2DEnabled = !!i697[3]
  i696.spriteEnabled = !!i697[4]
  i696.uiEnabled = !!i697[5]
  i696.textMeshProEnabled = !!i697[6]
  i696.tk2DEnabled = !!i697[7]
  i696.deAudioEnabled = !!i697[8]
  i696.deUnityExtendedEnabled = !!i697[9]
  i696.epoOutlineEnabled = !!i697[10]
  return i696
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources"] = function (request, data, root) {
  var i698 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources' )
  var i699 = data
  var i701 = i699[0]
  var i700 = []
  for(var i = 0; i < i701.length; i += 1) {
    i700.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.Resources+File', i701[i + 0]) );
  }
  i698.files = i700
  i698.componentToPrefabIds = i699[1]
  return i698
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Resources+File"] = function (request, data, root) {
  var i704 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Resources+File' )
  var i705 = data
  i704.path = i705[0]
  request.r(i705[1], i705[2], 0, i704, 'unityObject')
  return i704
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings"] = function (request, data, root) {
  var i706 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings' )
  var i707 = data
  var i709 = i707[0]
  var i708 = []
  for(var i = 0; i < i709.length; i += 1) {
    i708.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder', i709[i + 0]) );
  }
  i706.scriptsExecutionOrder = i708
  var i711 = i707[1]
  var i710 = []
  for(var i = 0; i < i711.length; i += 1) {
    i710.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer', i711[i + 0]) );
  }
  i706.sortingLayers = i710
  var i713 = i707[2]
  var i712 = []
  for(var i = 0; i < i713.length; i += 1) {
    i712.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer', i713[i + 0]) );
  }
  i706.cullingLayers = i712
  i706.timeSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings', i707[3], i706.timeSettings)
  i706.physicsSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings', i707[4], i706.physicsSettings)
  i706.physics2DSettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings', i707[5], i706.physics2DSettings)
  i706.qualitySettings = request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i707[6], i706.qualitySettings)
  i706.enableRealtimeShadows = !!i707[7]
  i706.enableAutoInstancing = !!i707[8]
  i706.enableStaticBatching = !!i707[9]
  i706.enableDynamicBatching = !!i707[10]
  i706.lightmapEncodingQuality = i707[11]
  i706.desiredColorSpace = i707[12]
  var i715 = i707[13]
  var i714 = []
  for(var i = 0; i < i715.length; i += 1) {
    i714.push( i715[i + 0] );
  }
  i706.allTags = i714
  return i706
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder"] = function (request, data, root) {
  var i718 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder' )
  var i719 = data
  i718.name = i719[0]
  i718.value = i719[1]
  return i718
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer"] = function (request, data, root) {
  var i722 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer' )
  var i723 = data
  i722.id = i723[0]
  i722.name = i723[1]
  i722.value = i723[2]
  return i722
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer"] = function (request, data, root) {
  var i726 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer' )
  var i727 = data
  i726.id = i727[0]
  i726.name = i727[1]
  return i726
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings"] = function (request, data, root) {
  var i728 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings' )
  var i729 = data
  i728.fixedDeltaTime = i729[0]
  i728.maximumDeltaTime = i729[1]
  i728.timeScale = i729[2]
  i728.maximumParticleTimestep = i729[3]
  return i728
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings"] = function (request, data, root) {
  var i730 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings' )
  var i731 = data
  i730.gravity = new pc.Vec3( i731[0], i731[1], i731[2] )
  i730.defaultSolverIterations = i731[3]
  i730.bounceThreshold = i731[4]
  i730.autoSyncTransforms = !!i731[5]
  i730.autoSimulation = !!i731[6]
  var i733 = i731[7]
  var i732 = []
  for(var i = 0; i < i733.length; i += 1) {
    i732.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask', i733[i + 0]) );
  }
  i730.collisionMatrix = i732
  return i730
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask"] = function (request, data, root) {
  var i736 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask' )
  var i737 = data
  i736.enabled = !!i737[0]
  i736.layerId = i737[1]
  i736.otherLayerId = i737[2]
  return i736
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings"] = function (request, data, root) {
  var i738 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings' )
  var i739 = data
  request.r(i739[0], i739[1], 0, i738, 'material')
  i738.gravity = new pc.Vec2( i739[2], i739[3] )
  i738.positionIterations = i739[4]
  i738.velocityIterations = i739[5]
  i738.velocityThreshold = i739[6]
  i738.maxLinearCorrection = i739[7]
  i738.maxAngularCorrection = i739[8]
  i738.maxTranslationSpeed = i739[9]
  i738.maxRotationSpeed = i739[10]
  i738.baumgarteScale = i739[11]
  i738.baumgarteTOIScale = i739[12]
  i738.timeToSleep = i739[13]
  i738.linearSleepTolerance = i739[14]
  i738.angularSleepTolerance = i739[15]
  i738.defaultContactOffset = i739[16]
  i738.autoSimulation = !!i739[17]
  i738.queriesHitTriggers = !!i739[18]
  i738.queriesStartInColliders = !!i739[19]
  i738.callbacksOnDisable = !!i739[20]
  i738.reuseCollisionCallbacks = !!i739[21]
  i738.autoSyncTransforms = !!i739[22]
  var i741 = i739[23]
  var i740 = []
  for(var i = 0; i < i741.length; i += 1) {
    i740.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask', i741[i + 0]) );
  }
  i738.collisionMatrix = i740
  return i738
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask"] = function (request, data, root) {
  var i744 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask' )
  var i745 = data
  i744.enabled = !!i745[0]
  i744.layerId = i745[1]
  i744.otherLayerId = i745[2]
  return i744
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.QualitySettings"] = function (request, data, root) {
  var i746 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.QualitySettings' )
  var i747 = data
  var i749 = i747[0]
  var i748 = []
  for(var i = 0; i < i749.length; i += 1) {
    i748.push( request.d('Luna.Unity.DTO.UnityEngine.Assets.QualitySettings', i749[i + 0]) );
  }
  i746.qualityLevels = i748
  var i751 = i747[1]
  var i750 = []
  for(var i = 0; i < i751.length; i += 1) {
    i750.push( i751[i + 0] );
  }
  i746.names = i750
  i746.shadows = i747[2]
  i746.anisotropicFiltering = i747[3]
  i746.antiAliasing = i747[4]
  i746.lodBias = i747[5]
  i746.shadowCascades = i747[6]
  i746.shadowDistance = i747[7]
  i746.shadowmaskMode = i747[8]
  i746.shadowProjection = i747[9]
  i746.shadowResolution = i747[10]
  i746.softParticles = !!i747[11]
  i746.softVegetation = !!i747[12]
  i746.activeColorSpace = i747[13]
  i746.desiredColorSpace = i747[14]
  i746.masterTextureLimit = i747[15]
  i746.maxQueuedFrames = i747[16]
  i746.particleRaycastBudget = i747[17]
  i746.pixelLightCount = i747[18]
  i746.realtimeReflectionProbes = !!i747[19]
  i746.shadowCascade2Split = i747[20]
  i746.shadowCascade4Split = new pc.Vec3( i747[21], i747[22], i747[23] )
  i746.streamingMipmapsActive = !!i747[24]
  i746.vSyncCount = i747[25]
  i746.asyncUploadBufferSize = i747[26]
  i746.asyncUploadTimeSlice = i747[27]
  i746.billboardsFaceCameraPosition = !!i747[28]
  i746.shadowNearPlaneOffset = i747[29]
  i746.streamingMipmapsMemoryBudget = i747[30]
  i746.maximumLODLevel = i747[31]
  i746.streamingMipmapsAddAllCameras = !!i747[32]
  i746.streamingMipmapsMaxLevelReduction = i747[33]
  i746.streamingMipmapsRenderersPerFrame = i747[34]
  i746.resolutionScalingFixedDPIFactor = i747[35]
  i746.streamingMipmapsMaxFileIORequests = i747[36]
  i746.currentQualityLevel = i747[37]
  return i746
}

Deserializers["Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame"] = function (request, data, root) {
  var i756 = root || request.c( 'Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame' )
  var i757 = data
  i756.weight = i757[0]
  i756.vertices = i757[1]
  i756.normals = i757[2]
  i756.tangents = i757[3]
  return i756
}

Deserializers["UnityEngine.Events.ArgumentCache"] = function (request, data, root) {
  var i758 = root || request.c( 'UnityEngine.Events.ArgumentCache' )
  var i759 = data
  request.r(i759[0], i759[1], 0, i758, 'm_ObjectArgument')
  i758.m_ObjectArgumentAssemblyTypeName = i759[2]
  i758.m_IntArgument = i759[3]
  i758.m_FloatArgument = i759[4]
  i758.m_StringArgument = i759[5]
  i758.m_BoolArgument = !!i759[6]
  return i758
}

Deserializers.fields = {"Luna.Unity.DTO.UnityEngine.Components.Transform":{"position":0,"scale":3,"rotation":6},"Luna.Unity.DTO.UnityEngine.Components.MeshFilter":{"sharedMesh":0},"Luna.Unity.DTO.UnityEngine.Components.MeshRenderer":{"additionalVertexStreams":0,"enabled":2,"sharedMaterial":3,"sharedMaterials":5,"receiveShadows":6,"shadowCastingMode":7,"sortingLayerID":8,"sortingOrder":9,"lightmapIndex":10,"lightmapSceneIndex":11,"lightmapScaleOffset":12,"lightProbeUsage":16,"reflectionProbeUsage":17},"Luna.Unity.DTO.UnityEngine.Scene.GameObject":{"name":0,"tagId":1,"enabled":2,"isStatic":3,"layer":4},"Luna.Unity.DTO.UnityEngine.Assets.Mesh":{"name":0,"halfPrecision":1,"useSimplification":2,"useUInt32IndexFormat":3,"vertexCount":4,"aabb":5,"streams":6,"vertices":7,"subMeshes":8,"bindposes":9,"blendShapes":10},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+SubMesh":{"triangles":0},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShape":{"name":0,"frames":1},"Luna.Unity.DTO.UnityEngine.Assets.Material":{"name":0,"shader":1,"renderQueue":3,"enableInstancing":4,"floatParameters":5,"colorParameters":6,"vectorParameters":7,"textureParameters":8,"materialFlags":9},"Luna.Unity.DTO.UnityEngine.Assets.Material+FloatParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+ColorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+VectorParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+TextureParameter":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Material+MaterialFlag":{"name":0,"enabled":1},"Luna.Unity.DTO.UnityEngine.Components.SphereCollider":{"center":0,"radius":3,"enabled":4,"isTrigger":5,"material":6},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystem":{"main":0,"colorBySpeed":1,"colorOverLifetime":2,"emission":3,"rotationBySpeed":4,"rotationOverLifetime":5,"shape":6,"sizeBySpeed":7,"sizeOverLifetime":8,"textureSheetAnimation":9,"velocityOverLifetime":10,"noise":11,"inheritVelocity":12,"forceOverLifetime":13,"limitVelocityOverLifetime":14,"useAutoRandomSeed":15,"randomSeed":16},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.MainModule":{"duration":0,"loop":1,"prewarm":2,"startDelay":3,"startLifetime":4,"startSpeed":5,"startSize3D":6,"startSizeX":7,"startSizeY":8,"startSizeZ":9,"startRotation3D":10,"startRotationX":11,"startRotationY":12,"startRotationZ":13,"startColor":14,"gravityModifier":15,"simulationSpace":16,"customSimulationSpace":17,"simulationSpeed":19,"useUnscaledTime":20,"scalingMode":21,"playOnAwake":22,"maxParticles":23,"emitterVelocityMode":24,"stopAction":25},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxCurve":{"mode":0,"curveMin":1,"curveMax":2,"curveMultiplier":3,"constantMin":4,"constantMax":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.MinMaxGradient":{"mode":0,"gradientMin":1,"gradientMax":2,"colorMin":3,"colorMax":7},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Gradient":{"mode":0,"colorKeys":1,"alphaKeys":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorBySpeedModule":{"enabled":0,"color":1,"range":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientColorKey":{"color":0,"time":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Data.GradientAlphaKey":{"alpha":0,"time":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ColorOverLifetimeModule":{"enabled":0,"color":1},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.EmissionModule":{"enabled":0,"rateOverTime":1,"rateOverDistance":2,"bursts":3},"Luna.Unity.DTO.UnityEngine.ParticleSystemTypes.Burst":{"count":0,"cycleCount":1,"minCount":2,"maxCount":3,"repeatInterval":4,"time":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.RotationOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ShapeModule":{"enabled":0,"shapeType":1,"randomDirectionAmount":2,"sphericalDirectionAmount":3,"randomPositionAmount":4,"alignToDirection":5,"radius":6,"radiusMode":7,"radiusSpread":8,"radiusSpeed":9,"radiusThickness":10,"angle":11,"length":12,"boxThickness":13,"meshShapeType":16,"mesh":17,"meshRenderer":19,"skinnedMeshRenderer":21,"useMeshMaterialIndex":23,"meshMaterialIndex":24,"useMeshColors":25,"normalOffset":26,"arc":27,"arcMode":28,"arcSpread":29,"arcSpeed":30,"donutRadius":31,"position":32,"rotation":35,"scale":38},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeBySpeedModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4,"range":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.SizeOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"separateAxes":4},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.TextureSheetAnimationModule":{"enabled":0,"mode":1,"animation":2,"numTilesX":3,"numTilesY":4,"useRandomRow":5,"frameOverTime":6,"startFrame":7,"cycleCount":8,"rowIndex":9,"flipU":10,"flipV":11,"spriteCount":12,"sprites":13},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.VelocityOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"radial":4,"speedModifier":5,"space":6,"orbitalX":7,"orbitalY":8,"orbitalZ":9,"orbitalOffsetX":10,"orbitalOffsetY":11,"orbitalOffsetZ":12},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.NoiseModule":{"enabled":0,"separateAxes":1,"strengthX":2,"strengthY":3,"strengthZ":4,"frequency":5,"damping":6,"octaveCount":7,"octaveMultiplier":8,"octaveScale":9,"quality":10,"scrollSpeed":11,"scrollSpeedMultiplier":12,"remapEnabled":13,"remapX":14,"remapY":15,"remapZ":16,"positionAmount":17,"rotationAmount":18,"sizeAmount":19},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.InheritVelocityModule":{"enabled":0,"mode":1,"curve":2},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.ForceOverLifetimeModule":{"enabled":0,"x":1,"y":2,"z":3,"space":4,"randomized":5},"Luna.Unity.DTO.UnityEngine.ParticleSystemModules.LimitVelocityOverLifetimeModule":{"enabled":0,"limit":1,"limitX":2,"limitY":3,"limitZ":4,"dampen":5,"separateAxes":6,"space":7,"drag":8,"multiplyDragByParticleSize":9,"multiplyDragByParticleVelocity":10},"Luna.Unity.DTO.UnityEngine.Components.ParticleSystemRenderer":{"mesh":0,"meshCount":2,"activeVertexStreamsCount":3,"alignment":4,"renderMode":5,"sortMode":6,"lengthScale":7,"velocityScale":8,"cameraVelocityScale":9,"normalDirection":10,"sortingFudge":11,"minParticleSize":12,"maxParticleSize":13,"pivot":14,"trailMaterial":17,"applyActiveColorSpace":19,"enabled":20,"sharedMaterial":21,"sharedMaterials":23,"receiveShadows":24,"shadowCastingMode":25,"sortingLayerID":26,"sortingOrder":27,"lightmapIndex":28,"lightmapSceneIndex":29,"lightmapScaleOffset":30,"lightProbeUsage":34,"reflectionProbeUsage":35},"Luna.Unity.DTO.UnityEngine.Textures.Texture2D":{"name":0,"width":1,"height":2,"mipmapCount":3,"anisoLevel":4,"filterMode":5,"hdr":6,"format":7,"wrapMode":8,"alphaIsTransparency":9,"alphaSource":10,"graphicsFormat":11,"sRGBTexture":12,"desiredColorSpace":13,"wrapU":14,"wrapV":15},"Luna.Unity.DTO.UnityEngine.Textures.Cubemap":{"name":0,"atlasId":1,"mipmapCount":2,"hdr":3,"size":4,"anisoLevel":5,"filterMode":6,"rects":7,"wrapU":8,"wrapV":9},"Luna.Unity.DTO.UnityEngine.Scene.Scene":{"name":0,"index":1,"startup":2},"Luna.Unity.DTO.UnityEngine.Components.Camera":{"aspect":0,"orthographic":1,"orthographicSize":2,"backgroundColor":3,"nearClipPlane":7,"farClipPlane":8,"fieldOfView":9,"depth":10,"clearFlags":11,"cullingMask":12,"rect":13,"targetTexture":14,"usePhysicalProperties":16,"focalLength":17,"sensorSize":18,"lensShift":20,"gateFit":22,"commandBufferCount":23,"cameraType":24,"enabled":25},"Luna.Unity.DTO.UnityEngine.Components.Light":{"type":0,"color":1,"cullingMask":5,"intensity":6,"range":7,"spotAngle":8,"shadows":9,"shadowNormalBias":10,"shadowBias":11,"shadowStrength":12,"shadowResolution":13,"lightmapBakeType":14,"renderMode":15,"cookie":16,"cookieSize":18,"shadowNearPlane":19,"enabled":20},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings":{"ambientIntensity":0,"reflectionIntensity":1,"ambientMode":2,"ambientLight":3,"ambientSkyColor":7,"ambientGroundColor":11,"ambientEquatorColor":15,"fogColor":19,"fogEndDistance":23,"fogStartDistance":24,"fogDensity":25,"fog":26,"skybox":27,"fogMode":29,"lightmaps":30,"lightProbes":31,"lightmapsMode":32,"mixedBakeMode":33,"environmentLightingMode":34,"ambientProbe":35,"referenceAmbientProbe":36,"useReferenceAmbientProbe":37,"customReflection":38,"defaultReflection":40,"defaultReflectionMode":42,"defaultReflectionResolution":43,"sunLightObjectId":44,"pixelLightCount":45,"defaultReflectionHDR":46,"hasLightDataAsset":47,"hasManualGenerate":48},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+Lightmap":{"lightmapColor":0,"lightmapDirection":2},"Luna.Unity.DTO.UnityEngine.Assets.RenderSettings+LightProbes":{"bakedProbes":0,"positions":1,"hullRays":2,"tetrahedra":3,"neighbours":4,"matrices":5},"Luna.Unity.DTO.UnityEngine.Assets.UniversalRenderPipelineAsset":{"AdditionalLightsPerObjectLimit":0,"AdditionalLightsRenderingMode":1,"LightRenderingMode":2,"ColorGradingLutSize":3,"ColorGradingMode":4,"MainLightRenderingMode":5,"MainLightRenderingModeValue":6,"SupportsMainLightShadows":7,"MixedLightingSupported":8,"MsaaQuality":9,"MSAA":10,"OpaqueDownsampling":11,"MainLightShadowmapResolution":12,"MainLightShadowmapResolutionValue":13,"SupportsSoftShadows":14,"SoftShadowQuality":15,"SoftShadowQualityValue":16,"ShadowDistance":17,"ShadowCascadeCount":18,"Cascade2Split":19,"Cascade3Split":20,"Cascade4Split":22,"CascadeBorder":25,"ShadowDepthBias":26,"ShadowNormalBias":27,"RenderScale":28,"RequireDepthTexture":29,"RequireOpaqueTexture":30,"SupportsHDR":31,"SupportsTerrainHoles":32},"Luna.Unity.DTO.UnityEngine.Assets.LightRenderingMode":{"Disabled":0,"PerVertex":1,"PerPixel":2},"Luna.Unity.DTO.UnityEngine.Assets.ColorGradingMode":{"LowDynamicRange":0,"HighDynamicRange":1},"Luna.Unity.DTO.UnityEngine.Assets.MsaaQuality":{"Disabled":0,"_2x":1,"_4x":2,"_8x":3},"Luna.Unity.DTO.UnityEngine.Assets.Downsampling":{"None":0,"_2xBilinear":1,"_4xBox":2,"_4xBilinear":3},"Luna.Unity.DTO.UnityEngine.Assets.ShadowResolution":{"_256":0,"_512":1,"_1024":2,"_2048":3,"_4096":4},"Luna.Unity.DTO.UnityEngine.Assets.SoftShadowQuality":{"UsePipelineSettings":0,"Low":1,"Medium":2,"High":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader":{"ShaderCompilationErrors":0,"name":1,"guid":2,"shaderDefinedKeywords":3,"passes":4,"usePasses":5,"defaultParameterValues":6,"unityFallbackShader":7,"readDepth":9,"hasDepthOnlyPass":10,"isCreatedByShaderGraph":11,"disableBatching":12,"compiled":13},"Luna.Unity.DTO.UnityEngine.Assets.Shader+ShaderCompilationError":{"shaderName":0,"errorMessage":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass":{"id":0,"subShaderIndex":1,"name":2,"passType":3,"grabPassTextureName":4,"usePass":5,"zTest":6,"zWrite":7,"culling":8,"blending":9,"alphaBlending":10,"colorWriteMask":11,"offsetUnits":12,"offsetFactor":13,"stencilRef":14,"stencilReadMask":15,"stencilWriteMask":16,"stencilOp":17,"stencilOpFront":18,"stencilOpBack":19,"tags":20,"passDefinedKeywords":21,"passDefinedKeywordGroups":22,"variants":23,"excludedVariants":24,"hasDepthReader":25},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Value":{"val":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Blending":{"src":0,"dst":1,"op":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+StencilOp":{"pass":0,"fail":1,"zFail":2,"comp":3},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Tag":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+KeywordGroup":{"keywords":0,"hasDiscard":1},"Luna.Unity.DTO.UnityEngine.Assets.Shader+Pass+Variant":{"passId":0,"subShaderIndex":1,"keywords":2,"vertexProgram":3,"fragmentProgram":4,"exportedForWebGl2":5,"readDepth":6},"Luna.Unity.DTO.UnityEngine.Assets.Shader+UsePass":{"shader":0,"pass":2},"Luna.Unity.DTO.UnityEngine.Assets.Shader+DefaultParameterValue":{"name":0,"type":1,"value":2,"textureValue":6,"shaderPropertyFlag":7},"Luna.Unity.DTO.UnityEngine.Assets.Resources":{"files":0,"componentToPrefabIds":1},"Luna.Unity.DTO.UnityEngine.Assets.Resources+File":{"path":0,"unityObject":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings":{"scriptsExecutionOrder":0,"sortingLayers":1,"cullingLayers":2,"timeSettings":3,"physicsSettings":4,"physics2DSettings":5,"qualitySettings":6,"enableRealtimeShadows":7,"enableAutoInstancing":8,"enableStaticBatching":9,"enableDynamicBatching":10,"lightmapEncodingQuality":11,"desiredColorSpace":12,"allTags":13},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+ScriptsExecutionOrder":{"name":0,"value":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+SortingLayer":{"id":0,"name":1,"value":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+CullingLayer":{"id":0,"name":1},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+TimeSettings":{"fixedDeltaTime":0,"maximumDeltaTime":1,"timeScale":2,"maximumParticleTimestep":3},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings":{"gravity":0,"defaultSolverIterations":3,"bounceThreshold":4,"autoSyncTransforms":5,"autoSimulation":6,"collisionMatrix":7},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+PhysicsSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings":{"material":0,"gravity":2,"positionIterations":4,"velocityIterations":5,"velocityThreshold":6,"maxLinearCorrection":7,"maxAngularCorrection":8,"maxTranslationSpeed":9,"maxRotationSpeed":10,"baumgarteScale":11,"baumgarteTOIScale":12,"timeToSleep":13,"linearSleepTolerance":14,"angularSleepTolerance":15,"defaultContactOffset":16,"autoSimulation":17,"queriesHitTriggers":18,"queriesStartInColliders":19,"callbacksOnDisable":20,"reuseCollisionCallbacks":21,"autoSyncTransforms":22,"collisionMatrix":23},"Luna.Unity.DTO.UnityEngine.Assets.ProjectSettings+Physics2DSettings+CollisionMask":{"enabled":0,"layerId":1,"otherLayerId":2},"Luna.Unity.DTO.UnityEngine.Assets.QualitySettings":{"qualityLevels":0,"names":1,"shadows":2,"anisotropicFiltering":3,"antiAliasing":4,"lodBias":5,"shadowCascades":6,"shadowDistance":7,"shadowmaskMode":8,"shadowProjection":9,"shadowResolution":10,"softParticles":11,"softVegetation":12,"activeColorSpace":13,"desiredColorSpace":14,"masterTextureLimit":15,"maxQueuedFrames":16,"particleRaycastBudget":17,"pixelLightCount":18,"realtimeReflectionProbes":19,"shadowCascade2Split":20,"shadowCascade4Split":21,"streamingMipmapsActive":24,"vSyncCount":25,"asyncUploadBufferSize":26,"asyncUploadTimeSlice":27,"billboardsFaceCameraPosition":28,"shadowNearPlaneOffset":29,"streamingMipmapsMemoryBudget":30,"maximumLODLevel":31,"streamingMipmapsAddAllCameras":32,"streamingMipmapsMaxLevelReduction":33,"streamingMipmapsRenderersPerFrame":34,"resolutionScalingFixedDPIFactor":35,"streamingMipmapsMaxFileIORequests":36,"currentQualityLevel":37},"Luna.Unity.DTO.UnityEngine.Assets.Mesh+BlendShapeFrame":{"weight":0,"vertices":1,"normals":2,"tangents":3}}

Deserializers.requiredComponents = {"27":[28],"29":[28],"30":[28],"31":[28],"32":[28],"33":[28],"34":[35],"36":[14],"37":[38],"39":[38],"40":[38],"41":[38],"42":[38],"43":[38],"44":[38],"45":[46],"47":[46],"48":[46],"49":[46],"50":[46],"51":[46],"52":[46],"53":[46],"54":[46],"55":[46],"56":[46],"57":[46],"58":[46],"59":[14],"60":[3],"61":[62],"63":[62],"64":[65],"9":[66],"67":[65],"68":[14],"69":[14],"17":[16],"70":[71],"72":[65],"73":[65],"74":[64],"75":[76,65],"77":[65],"78":[64],"79":[65],"80":[65],"81":[65],"82":[65],"83":[65],"84":[65],"85":[65],"86":[65],"87":[65],"88":[76,65],"89":[65],"90":[65],"91":[65],"92":[65],"93":[76,65],"94":[65],"95":[96],"97":[96],"98":[96],"99":[96],"100":[14],"101":[14],"102":[71],"103":[96],"104":[64],"105":[65],"106":[3,65],"107":[65,76],"108":[65],"109":[76,65],"110":[3],"111":[76,65],"112":[65],"113":[71]}

Deserializers.types = ["UnityEngine.Transform","UnityEngine.MeshFilter","UnityEngine.Mesh","UnityEngine.MeshRenderer","UnityEngine.Material","UnityEngine.MonoBehaviour","HexCellView","UnityEngine.Shader","TileStackView","TileStackRoot","UnityEngine.SphereCollider","UnityEngine.ParticleSystem","UnityEngine.ParticleSystemRenderer","UnityEngine.Texture2D","UnityEngine.Camera","UnityEngine.AudioListener","UnityEngine.Light","UnityEngine.Rendering.Universal.UniversalAdditionalLightData","Zenject.SceneContext","GameplayInstaller","UnityEngine.Grid","TileConfig","GridDefinitionConfig","TileStackDragInput","TileStackSpawnPoint","UnityEngine.Cubemap","DG.Tweening.Core.DOTweenSettings","UnityEngine.AudioLowPassFilter","UnityEngine.AudioBehaviour","UnityEngine.AudioHighPassFilter","UnityEngine.AudioReverbFilter","UnityEngine.AudioDistortionFilter","UnityEngine.AudioEchoFilter","UnityEngine.AudioChorusFilter","UnityEngine.Cloth","UnityEngine.SkinnedMeshRenderer","UnityEngine.FlareLayer","UnityEngine.ConstantForce","UnityEngine.Rigidbody","UnityEngine.Joint","UnityEngine.HingeJoint","UnityEngine.SpringJoint","UnityEngine.FixedJoint","UnityEngine.CharacterJoint","UnityEngine.ConfigurableJoint","UnityEngine.CompositeCollider2D","UnityEngine.Rigidbody2D","UnityEngine.Joint2D","UnityEngine.AnchoredJoint2D","UnityEngine.SpringJoint2D","UnityEngine.DistanceJoint2D","UnityEngine.FrictionJoint2D","UnityEngine.HingeJoint2D","UnityEngine.RelativeJoint2D","UnityEngine.SliderJoint2D","UnityEngine.TargetJoint2D","UnityEngine.FixedJoint2D","UnityEngine.WheelJoint2D","UnityEngine.ConstantForce2D","UnityEngine.StreamingController","UnityEngine.TextMesh","UnityEngine.Tilemaps.TilemapRenderer","UnityEngine.Tilemaps.Tilemap","UnityEngine.Tilemaps.TilemapCollider2D","UnityEngine.Canvas","UnityEngine.RectTransform","UnityEngine.Collider","UnityEngine.Rendering.UI.UIFoldout","UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera","UnityEngine.Rendering.Universal.UniversalAdditionalCameraData","Unity.VisualScripting.SceneVariables","Unity.VisualScripting.Variables","UnityEngine.UI.Dropdown","UnityEngine.UI.Graphic","UnityEngine.UI.GraphicRaycaster","UnityEngine.UI.Image","UnityEngine.CanvasRenderer","UnityEngine.UI.AspectRatioFitter","UnityEngine.UI.CanvasScaler","UnityEngine.UI.ContentSizeFitter","UnityEngine.UI.GridLayoutGroup","UnityEngine.UI.HorizontalLayoutGroup","UnityEngine.UI.HorizontalOrVerticalLayoutGroup","UnityEngine.UI.LayoutElement","UnityEngine.UI.LayoutGroup","UnityEngine.UI.VerticalLayoutGroup","UnityEngine.UI.Mask","UnityEngine.UI.MaskableGraphic","UnityEngine.UI.RawImage","UnityEngine.UI.RectMask2D","UnityEngine.UI.Scrollbar","UnityEngine.UI.ScrollRect","UnityEngine.UI.Slider","UnityEngine.UI.Text","UnityEngine.UI.Toggle","UnityEngine.EventSystems.BaseInputModule","UnityEngine.EventSystems.EventSystem","UnityEngine.EventSystems.PointerInputModule","UnityEngine.EventSystems.StandaloneInputModule","UnityEngine.EventSystems.TouchInputModule","UnityEngine.EventSystems.Physics2DRaycaster","UnityEngine.EventSystems.PhysicsRaycaster","Unity.VisualScripting.ScriptMachine","UnityEngine.InputSystem.UI.InputSystemUIInputModule","UnityEngine.InputSystem.UI.TrackedDeviceRaycaster","TMPro.TextContainer","TMPro.TextMeshPro","TMPro.TextMeshProUGUI","TMPro.TMP_Dropdown","TMPro.TMP_SelectionCaret","TMPro.TMP_SubMesh","TMPro.TMP_SubMeshUI","TMPro.TMP_Text","Unity.VisualScripting.StateMachine"]

Deserializers.unityVersion = "2022.3.62f2";

Deserializers.productName = "test_3d";

Deserializers.lunaInitializationTime = "12/03/2025 22:18:18";

Deserializers.lunaDaysRunning = "0.0";

Deserializers.lunaVersion = "7.0.0";

Deserializers.lunaSHA = "3bcc3e343f23b4c67e768a811a8d088c7f7adbc5";

Deserializers.creativeName = "";

Deserializers.lunaAppID = "35041";

Deserializers.projectId = "9db75f8e0a880a448be84f3a7a50b82c";

Deserializers.packagesInfo = "com.unity.inputsystem: 1.14.2\ncom.unity.render-pipelines.universal: 14.0.12\ncom.unity.textmeshpro: 3.0.7\ncom.unity.timeline: 1.7.7\ncom.unity.ugui: 1.0.0";

Deserializers.externalJsLibraries = "";

Deserializers.androidLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.androidLink?window.$environment.packageConfig.androidLink:'Empty';

Deserializers.iosLink = ( typeof window !== "undefined")&&window.$environment.packageConfig.iosLink?window.$environment.packageConfig.iosLink:'Empty';

Deserializers.base64Enabled = "False";

Deserializers.minifyEnabled = "True";

Deserializers.isForceUncompressed = "False";

Deserializers.isAntiAliasingEnabled = "False";

Deserializers.isRuntimeAnalysisEnabledForCode = "True";

Deserializers.runtimeAnalysisExcludedClassesCount = "0";

Deserializers.runtimeAnalysisExcludedMethodsCount = "0";

Deserializers.runtimeAnalysisExcludedModules = "";

Deserializers.isRuntimeAnalysisEnabledForShaders = "True";

Deserializers.isRealtimeShadowsEnabled = "False";

Deserializers.isReferenceAmbientProbeBaked = "False";

Deserializers.isLunaCompilerV2Used = "True";

Deserializers.companyName = "DefaultCompany";

Deserializers.buildPlatform = "WebGL";

Deserializers.applicationIdentifier = "com.DefaultCompany.test-3d";

Deserializers.disableAntiAliasing = true;

Deserializers.graphicsConstraint = 24;

Deserializers.linearColorSpace = true;

Deserializers.buildID = "cd73c337-93e8-49bb-bc58-cd60e37780d2";

Deserializers.runtimeInitializeOnLoadInfos = [[["UnityEngine","Rendering","DebugUpdater","RuntimeInit"],["UnityEngine","Experimental","Rendering","ScriptableRuntimeReflectionSystemSettings","ScriptingDirtyReflectionSystemInstance"]],[["UnityEngine","InputSystem","InputSystem","RunInitialUpdate"],["Unity","VisualScripting","RuntimeVSUsageUtility","RuntimeInitializeOnLoadBeforeSceneLoad"]],[],[["UnityEngine","Experimental","Rendering","XRSystem","XRSystemInit"]],[["UnityEngine","InputSystem","UI","InputSystemUIInputModule","ResetDefaultActions"],["UnityEngine","InputSystem","InputSystem","RunInitializeInPlayer"]]];

Deserializers.typeNameToIdMap = function(){ var i = 0; return Deserializers.types.reduce( function( res, item ) { res[ item ] = i++; return res; }, {} ) }()

